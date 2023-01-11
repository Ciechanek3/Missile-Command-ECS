using Math;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Collision
{
    [BurstCompile]
    public partial struct DestroyEntityJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;
        public Entity Entity;
        public float3 EntityPosition;
        public float3 TargetPosition;
        public float2 Offset;
            
        [BurstCompile]
        private void Execute()
        {
            if (MathHelpers.CheckIfFloatIsInArea(EntityPosition, TargetPosition, Offset))
            {
                Ecb.DestroyEntity(Entity);
            }
        }
    }
}