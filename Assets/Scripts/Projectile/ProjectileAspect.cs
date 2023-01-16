using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Projectile
{
    public readonly partial struct ProjectileAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;

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
            TransformAspect.Position += TransformAspect.Up * MovementSpeed * deltaTime;
        }

        public void SetDestination(float2 destination)
        {
            if (Destination.x == 0 && Destination.y == 0)
            {
                Destination = destination;
            }
        }

        public bool CheckIfOnDestination()
        {
            if (System.Math.Abs(TransformAspect.Position.x - Destination.x) < 0.1)
            {
                if (System.Math.Abs(TransformAspect.Position.y - Destination.y) < 0.1)
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
                Position = TransformAspect.Position,
                Rotation = quaternion.identity,
                Scale = 1f
            };
        }
    }
}
