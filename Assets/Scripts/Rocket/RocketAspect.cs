using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Rocket
{
    public readonly partial struct RocketAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly TransformAspect _transformAspect;
        
        private readonly RefRO<RocketProperties> _rocketBehavior;

        public TransformAspect TransformAspect => _transformAspect;

        private float MovementSpeed => _rocketBehavior.ValueRO.Speed;
        public void Move(float speedMultiplier, float deltaTime)
        {
            _transformAspect.Position += TransformAspect.Up * MovementSpeed * speedMultiplier * deltaTime;
        }
    }
}
