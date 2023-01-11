using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Rocket
{
    public readonly partial struct RocketAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly TransformAspect TransformAspect;
        private readonly RefRO<RocketBehavior> _rocketBehavior;

        private float MovementSpeed => _rocketBehavior.ValueRO.Speed;
        public void Move(float speedMultiplier, float deltaTime)
        {
            TransformAspect.Position += TransformAspect.Up * MovementSpeed * speedMultiplier * deltaTime;
        }
    }
}
