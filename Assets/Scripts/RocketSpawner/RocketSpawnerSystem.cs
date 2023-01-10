using Rocket;
using Unity.Burst;
using Unity.Entities;

namespace RocketSpawner
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct RocketSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RocketSpawnerProperties>();
        }
        
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            Entity rsp = SystemAPI.GetSingletonEntity<RocketSpawnerProperties>();
            var rocketSpawner = SystemAPI.GetAspectRW<RocketSpawnerAspect>(rsp);
            
            new SpawnRocketJob
            {
                DeltaTime = deltaTime,
                Ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                USTransform = rocketSpawner.GetRandomRocketSpawn()
            }.Run();
        }
    }
}