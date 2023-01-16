using Unity.Entities;
using Unity.Mathematics;

namespace Movement
{
    public struct InputData : IComponentData
    {
        public float2 Movement;
        public float Shoot;
    }
}
