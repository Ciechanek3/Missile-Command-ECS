using Unity.Entities;
using Unity.Mathematics;

namespace Movement
{
    public struct InputData : IComponentData
    {
        public float2 Movement;
        public float Shoot;
        public bool Active1;
        public bool Active2;
        public bool Active3;
    }
}
