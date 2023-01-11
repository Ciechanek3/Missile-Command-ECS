using Unity.Entities;
using Unity.Mathematics;

namespace City
{
    public struct TargetTag : IComponentData
    {
        public float3 Position;
        public float2 Offset;
    }
}
