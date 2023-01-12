using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Projectile
{
    public readonly partial struct ProjectileAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;

        private readonly RefRO<ProjectileProperties> _projectileProperties;

        
        public void Fire(float2 destination)
        {
            TransformAspect.TranslateWorld(new float3(destination.x, destination.y, 0));
        }
    }
}
