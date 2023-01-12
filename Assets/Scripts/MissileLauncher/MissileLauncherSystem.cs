using System.Collections.Generic;
using Marker;
using Movement;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace MissileLauncher
{
    [BurstCompile]
    [UpdateAfter(typeof(MarkerSystem))]
    public partial class MissileLauncherSystem : SystemBase
    {
        private bool _isListCreated = false;
        private MissileLauncherAspect currentMissileLauncher;
        List<MissileLauncherAspect> msa = new List<MissileLauncherAspect>();
        [BurstCompile]
        protected override void OnUpdate()
        {
            if (_isListCreated == false)
            {
                _isListCreated = true;
                foreach (var ml in SystemAPI.Query<MissileLauncherAspect>())
                {
                    Debug.Log("ml");
                    msa.Add(ml);
                }

                if (msa.Count == 3)
                {
                    currentMissileLauncher = msa[1];
                }
            }
            
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);

            new FireProjectile
            {
                DeltaTime = deltaTime,
                MissileLauncher = currentMissileLauncher,
                Ecb = ecbSingleton.CreateCommandBuffer(World.Unmanaged),
                USTransform = currentMissileLauncher.GetMissileSpawnPoint(),
            }.Run();

        }
    }
    
    [BurstCompile]
    public partial struct FireProjectile : IJobEntity
    {
        public float DeltaTime;
        public MissileLauncherAspect MissileLauncher;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;
        
        [BurstCompile]
        private void Execute()
        {
            MissileLauncher.ProjectileSpawnTimer -= DeltaTime;
            if (MissileLauncher.ShouldSpawnNewProjectile == false) return;
            if (MissileLauncher.Fire() == false) return;
            MissileLauncher.ProjectileSpawnTimer = MissileLauncher.Cooldown;
            var newRocket = Ecb.Instantiate(MissileLauncher.ProjectileEntity);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform{ Value = USTransform});
        }
    }
    
}
