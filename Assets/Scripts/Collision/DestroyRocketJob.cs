using City;
using Math;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Collision
{
    [BurstCompile]
    public partial struct DestroyRocketJob : IJobEntity
    {
        public EntityCommandBuffer Ecb;
        public Entity Entity;
        public float3 EntityPosition;
        public float3 TargetPosition;
        public float2 Offset;
            
        [BurstCompile]
        private void Execute()
        {
            if (MathHelpers.CheckIfFloatIsInSquareArea(EntityPosition, TargetPosition, Offset))
            {
                Ecb.DestroyEntity(Entity);
                
            }
        }
    }
}