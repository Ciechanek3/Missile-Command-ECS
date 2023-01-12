using MissileLauncher;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace RocketSpawner
{
    [BurstCompile]
    public partial struct SpawnRocketJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;
        
        [BurstCompile]
        private void Execute(RocketSpawnerAspect spawner)
        {
            spawner.ProjectileSpawnTimer -= DeltaTime;
            if (spawner.ShouldSpawnNewRocket == false) return;
            
            spawner.ProjectileSpawnTimer = spawner.SpawnDelay;
            var newRocket = Ecb.Instantiate(spawner.RocketPrefab);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform{ Value = USTransform});
        }
    }
}
