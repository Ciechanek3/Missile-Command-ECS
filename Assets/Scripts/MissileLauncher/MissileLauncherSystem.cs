using System.Collections.Generic;
using Marker;
using Math;
using Movement;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace MissileLauncher
{
    [BurstCompile]
    [UpdateAfter(typeof(MarkerSystem))]
    public partial class MissileLauncherSystem : SystemBase
    {
        private bool _isListCreated = false;
        private MissileLauncherAspect currentMissileLauncher;
        [BurstCompile]
        protected override void OnUpdate()
        {
            if (_isListCreated == false)
            {
                ChangeActiveLauncher(1);
            }

            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);

            /*new FireProjectile
            {
                DeltaTime = deltaTime,
                MissileLauncher = currentMissileLauncher,
                Ecb = ecbSingleton.CreateCommandBuffer(World.Unmanaged),
                USTransform = currentMissileLauncher.GetMissileSpawnPoint(),
            }.Run();*/

            foreach (var missileLauncher in SystemAPI.Query<MissileLauncherAspect>().WithAll<ActiveLauncherTag>())
            {
                missileLauncher.Fire();
            }
        }
        
        public void ChangeActiveLauncher(int index)
        {
            List<MissileLauncherAspect> missileLauncherAspects = new List<MissileLauncherAspect>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            _isListCreated = true;
            foreach (var missileLauncher in SystemAPI.Query<MissileLauncherAspect>())
            {
                missileLauncherAspects.Add(missileLauncher);
            }
                
            ecb.AddComponent<ActiveLauncherTag>(missileLauncherAspects[1].Entity);
            ecb.Playback(EntityManager);
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
