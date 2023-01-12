using System.Collections.Generic;
using Math;
using Movement;
using RocketSpawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace MissileLauncher
{
    [BurstCompile]
    [UpdateAfter(typeof(RocketSpawnerSystem))]
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
                    msa.Add(ml);
                }

                currentMissileLauncher = msa[1];
            }
            
            
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);

            new FireProjectile
            {
                DeltaTime = deltaTime,
                Cooldown = currentMissileLauncher.Cooldown,
                InitialAmmo = currentMissileLauncher.InitialAmmo,
                Ecb = ecbSingleton.CreateCommandBuffer(World.Unmanaged),
                USTransform = currentMissileLauncher.GetMissileSpawnPoint(),
            };

        }
    }
    
    [BurstCompile]
    public partial struct FireProjectile : IJobEntity
    {
        public float DeltaTime;
        public float Cooldown;
        public float InitialAmmo;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;
        
        [BurstCompile]
        private void Execute(RocketSpawnerAspect spawner)
        {
            spawner.RocketSpawnTimer -= DeltaTime;
            if (spawner.ShouldSpawnNewRocket == false) return;
            
            spawner.RocketSpawnTimer = spawner.SpawnDelay;
            var newRocket = Ecb.Instantiate(spawner.RocketPrefab);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform{ Value = USTransform});
        }
    }
    
}
