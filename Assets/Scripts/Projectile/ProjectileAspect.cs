using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Projectile
{
    public readonly partial struct ProjectileAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly TransformAspect _transformAspect;

        private readonly RefRO<ProjectileProperties> _projectileProperties;

        public void Fire(float2 destination)
        {
            _transformAspect.TranslateWorld(new float3(destination.x, destination.y, 0));
        }
    }
}
