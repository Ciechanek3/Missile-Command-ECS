using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MissileLauncher
{
    public struct MissileLauncherProperties : IComponentData
    {
        public Entity ProjectilePrefab;
        public float2 Position;
        public int Ammo;
        public float Cooldown;
    }
}
