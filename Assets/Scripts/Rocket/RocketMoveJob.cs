using Unity.Burst;
using Unity.Entities;

namespace Rocket
{
    [BurstCompile]
    public partial struct RocketMoveJob : IJobEntity
    {
        public float SpeedMultiplier;
        public float DeltaTime;
        [BurstCompile]
        private void Execute(RocketAspect rocket)
        {
            rocket.Move(SpeedMultiplier, DeltaTime);
        }
    }
}
