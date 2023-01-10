using Rocket.Aspect;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Rocket
{
    [BurstCompile]
    public partial struct SpawnRocketJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;
        
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
