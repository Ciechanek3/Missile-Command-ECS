using Unity.Entities;
using Unity.Mathematics;

namespace MissileLauncher
{
    public struct MissileLauncherProperties : IComponentData
    {
        public float2 FirePosition;
        public Entity ProjectilePrefab;
        public float Cooldown;
    }
}
