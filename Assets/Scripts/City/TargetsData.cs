using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace City
{
    public struct TargetsData : IComponentData
    {
        public NativeList<float3> Targets;
        public float2 TargetsOffset;
    }
}
