using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

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
            float2 position = new float2(_transformAspect.Position.x, _transformAspect.Position.y);
            Debug.Log((math.round(position) == math.round(Destination)) + " " +  math.round(position) + " " + math.round(Destination));
            if (math.all(math.round(position) == math.round(Destination)))
            {
                return true;
            }
            
            return false;
           
        }
    }
}
