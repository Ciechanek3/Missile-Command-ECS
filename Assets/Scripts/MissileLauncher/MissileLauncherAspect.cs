using Projectile;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace MissileLauncher
{
    public readonly partial struct MissileLauncherAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;

        private readonly RefRO<MissileLauncherProperties> _projectileProperties;

        public float2 FirePosition => _projectileProperties.ValueRO.FirePosition;
        public float Cooldown => _projectileProperties.ValueRO.Cooldown;
        public int InitialAmmo => _projectileProperties.ValueRO.InitialAmmo;

        public UniformScaleTransform GetMissileSpawnPoint()
        {
            float3 startingPosition = new float3(FirePosition.x, FirePosition.y, 0);
            return new UniformScaleTransform()
            {
                Position = startingPosition,
                Rotation = quaternion.identity,
                Scale = 1f
            };
        }
    }
}
