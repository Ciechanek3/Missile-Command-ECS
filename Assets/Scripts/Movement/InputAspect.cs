using Unity.Entities;
using Unity.Mathematics;

namespace Movement
{
    public readonly partial struct InputAspect : IAspect
    {
        public readonly RefRW<InputData> InputData;

        public float2 Movement
        {
            get => InputData.ValueRO.Movement;
            set => InputData.ValueRW.Movement = value;
        }

        public float Shooting
        {
            get => InputData.ValueRO.Shoot;
            set => InputData.ValueRW.Shoot = value;
        }
    }
}

