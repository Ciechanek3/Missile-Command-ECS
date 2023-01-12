using Unity.Entities;
using Unity.Mathematics;

namespace Movement
{
    public readonly partial struct InputAspect : IAspect
    {
        public readonly RefRW<InputData> InputData;

        public float2 Movement => InputData.ValueRW.Movement;

        public void ModifyMovement(float2 movement)
        {
            InputData.ValueRW.Movement = movement;
        }
    }
}

