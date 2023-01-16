using Projectile;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Explosion
{
    [BurstCompile]
    [UpdateAfter(typeof(ProjectileSystem))]
    public partial struct ExplosionSystem : ISystem
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
            float time = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new DestroyExplosion
            {
                DeltaTime = time,
                Ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct DestroyExplosion : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        private void Execute(ExplosionAspect explosion)
        {
            explosion.LastingDuration -= DeltaTime;
            if (explosion.ShouldDestroy == false) return;
            Ecb.DestroyEntity(explosion.Entity);
        }
    }
}
