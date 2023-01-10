using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Rocket
{
    public readonly partial struct RocketAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<RocketBehavior> _rocketBehavior;

        private float MovementSpeed => _rocketBehavior.ValueRO.Speed;
        public void Move(float speedMultiplier, float deltaTime)
        {
            _transformAspect.Position += _transformAspect.Up * MovementSpeed * speedMultiplier * deltaTime;
        }
    }
}
