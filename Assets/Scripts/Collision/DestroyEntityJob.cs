using City;
using Math;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Collision
{
    [BurstCompile]
    public partial struct DestroyEntityJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;
        public Entity Entity;

        [BurstCompile]
        private void Execute()
        {
            Ecb.DestroyEntity(Entity);
        }
    }
}