using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Projectile
{
    public readonly partial struct ProjectileAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly TransformAspect _transformAspect;

        private readonly RefRW<ProjectileProperties> _projectileProperties;

        private float MovementSpeed => _projectileProperties.ValueRO.MovementSpeed;
        private float2 Destination
        {
            get => _projectileProperties.ValueRO.Destination;
            set => _projectileProperties.ValueRW.Destination = value;
        }
        
        public Entity ExplosionEntity => _projectileProperties.ValueRO.ExplosionPrefab;

        public void Fire(float deltaTime)
        {
            _transformAspect.Position += _transformAspect.Up * MovementSpeed * deltaTime;
        }

        public void SetDestination(float2 destination)
        {
            Destination = destination;
        }

        public bool CheckIfOnDestination()
        {
            if (System.Math.Abs(_transformAspect.Position.x - Destination.x) < 0.01)
            {
                if (System.Math.Abs(_transformAspect.Position.y - Destination.y) < 0.01)
                {
                    return true; 
                }
            }
            
            return false;
        }

        public UniformScaleTransform GetProjectilePosition()
        {
            return new UniformScaleTransform()
            {
                Position = _transformAspect.Position,
                Rotation = quaternion.identity,
                Scale = 1f,
            };
        }
    }
}
