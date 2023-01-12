using City;
using Math;
using Rocket;
using RocketSpawner;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Collision
{
    [BurstCompile]
    [UpdateAfter(typeof(RocketMoveSystem))]
    public partial struct RocketCollisionSystem : ISystem
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

                    foreach (var target in SystemAPI.Query<TargetAspect>())
                    {
                        float3 objectPosition = MathHelpers.TransformAspectToFloat3(target.TransformAspect);
                        if (MathHelpers.CheckIfFloatIsInArea(rocketPosition, objectPosition, rocketSpawner.TargetsOffset))
                        {
                            ecb.DestroyEntity(target.Entity);
                        }
                    }
                    
                    new DestroyRocketJob
                    {
                        Ecb = ecb,
                        Entity = rocket.Entity,
                        EntityPosition = rocketPosition,
                        TargetPosition = targetPosition,
                        Offset = rocketSpawner.TargetsOffset
                    }.Run();
                    
                    
                }

            }
        }

        
    }
}
