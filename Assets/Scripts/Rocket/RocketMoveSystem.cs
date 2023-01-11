using City;
using RocketSpawner;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Rocket
{
    [BurstCompile]
    [UpdateAfter(typeof(RocketSpawnerSystem))]
    public partial struct RocketMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float speedMultiplier = Random.Range(0.5f, 3f);
            float deltaTime = SystemAPI.Time.DeltaTime;

            new RocketMoveJob
            {
                SpeedMultiplier = speedMultiplier,
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
}
