using Marker;
using MissileLauncher;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Projectile
{
    [BurstCompile]
    [UpdateAfter(typeof(MissileLauncherSystem))]
    public partial struct ProjectileSystem : ISystem
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
            Entity mp = SystemAPI.GetSingletonEntity<MarkerProperties>();
            var markerAspect = SystemAPI.GetAspectRW<MarkerAspect>(mp);
            float deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            new FireProjectileJob
            {
                Destination = markerAspect.Position,
                DeltaTime = deltaTime,
                Ecb = ecb
            }.Run();

            foreach (var projectile in SystemAPI.Query<ProjectileAspect>())
            {
                if(projectile.CheckIfOnDestination())
                {
                    new MakeExplosionJob
                    {
                        Ecb = ecb,
                    }.Run();
                }
            }
        }
    }
    
    [BurstCompile]
    public partial struct FireProjectileJob : IJobEntity
    {
        public float2 Destination;
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        
        [BurstCompile]
        private void Execute(ProjectileAspect projectile)
        {
            if (projectile.CheckIfOnDestination())
            {
                Ecb.DestroyEntity(projectile.Entity);
                return;
            }
            projectile.SetDestination(Destination);
            projectile.Fire(DeltaTime);
        }
    }
    
    [BurstCompile]
    public partial struct MakeExplosionJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        private void Execute(ProjectileAspect projectileAspect)
        {
            var newExplosion = Ecb.Instantiate(projectileAspect.ExplosionEntity);
            Ecb.SetComponent(newExplosion, new LocalToWorldTransform { Value = projectileAspect.GetProjectilePosition() });
        }
    }
}


