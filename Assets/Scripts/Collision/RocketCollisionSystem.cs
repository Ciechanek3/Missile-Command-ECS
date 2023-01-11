using City;
using Math;
using Rocket;
using RocketSpawner;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Collision
{
    [BurstCompile]
    [UpdateAfter(typeof(RocketMoveSystem))]
    public partial struct RocketCollisionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        
        }

        public void OnDestroy(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity rsp = SystemAPI.GetSingletonEntity<RocketSpawnerProperties>();
            var rocketSpawner = SystemAPI.GetAspectRW<RocketSpawnerAspect>(rsp);
            
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            
            foreach (var rocket in SystemAPI.Query<RocketAspect>())
            {
                for (int i = 0; i < rocketSpawner.Targets.Length; i++)
                {
                    float3 rocketPosition = MathHelpers.TransformAspectToFloat3(rocket.TransformAspect);
                    float3 targetPosition = rocketSpawner.Targets[i];

                    new DestroyEntityJob
                    {
                        Ecb = ecb,
                        Entity = rocket.Entity,
                        EntityPosition = rocketPosition,
                        TargetPosition = targetPosition,
                        Offset = rocketSpawner.TargetsOffset
                    }.Schedule();
                }
                
            }
        }

        
    }
}
