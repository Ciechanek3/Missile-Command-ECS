using City;
using Explosion;
using Math;
using Projectile;
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
            if(!SystemAPI.TryGetSingletonEntity<RocketSpawnerProperties>(out Entity rsp)) return;
            var rocketSpawner = SystemAPI.GetAspectRW<RocketSpawnerAspect>(rsp);

            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);


            foreach (var rocket in SystemAPI.Query<RocketAspect>())
            {
                for (int i = 0; i < rocketSpawner.Targets.Length; i++)
                {

                    float3 rocketPosition = MathHelpers.TransformAspectToFloat3(rocket.TransformAspect);

                    foreach (var target in SystemAPI.Query<TargetAspect>())
                    {
                        float3 objectPosition = MathHelpers.TransformAspectToFloat3(target.TransformAspect);
                        if (MathHelpers.CheckIfFloatIsInSquareArea(rocketPosition, objectPosition,
                                rocketSpawner.TargetsOffset))
                        {
                            ecb.DestroyEntity(target.Entity);
                            new DestroyEntityJob
                            {
                                Ecb = ecb,
                                Entity = rocket.Entity,
                            }.Run();
                        }
                    }

                    foreach (var projectile in SystemAPI.Query<ProjectileAspect>())
                    {
                        float3 objectPosition = MathHelpers.TransformAspectToFloat3(projectile.TransformAspect);
                        if (MathHelpers.CheckIfFloatIsInCircleArea(rocketPosition, objectPosition,
                                0.2f))
                        {
                            new DestroyEntityJob
                            {
                                Ecb = ecb,
                                Entity = rocket.Entity,
                            }.Run();
                        }
                    }

                    foreach (var explosion in SystemAPI.Query<ExplosionAspect>())
                    {
                        float3 objectPosition = MathHelpers.TransformAspectToFloat3(explosion.TransformAspect);
                        if (MathHelpers.CheckIfFloatIsInCircleArea(rocketPosition, objectPosition,
                                0.6f))
                        {
                            new DestroyEntityJob
                            {
                                Ecb = ecb,
                                Entity = rocket.Entity,
                            }.Run();
                        }
                    }

                }
            }
        }
    }
}
