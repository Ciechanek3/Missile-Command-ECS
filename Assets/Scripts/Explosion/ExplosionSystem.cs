using Projectile;
using Unity.Burst;
using Unity.Entities;

namespace Explosion
{
    [BurstCompile]
    [UpdateAfter(typeof(ProjectileSystem))]
    public partial struct ExplosionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {

        }

        public void OnDestroy(ref SystemState state)
        {

        }

        public void OnUpdate(ref SystemState state)
        {
            
        }
    }
}
