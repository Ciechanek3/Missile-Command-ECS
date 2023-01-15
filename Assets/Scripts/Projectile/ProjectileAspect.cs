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

        private float2 Destination
        {
            get => _projectileProperties.ValueRO.Destination;
            set => _projectileProperties.ValueRW.Destination = value;
        }

        public void Fire()
        {
            _transformAspect.TranslateWorld(new float3(Destination.x, Destination.y, 0));
        }

        public void SetDestination(float2 destination)
        {
            Destination = destination;
        }
    }
}
