using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MissileLauncher
{
    public struct MissileLauncherProperties : IComponentData
    {
        public Entity ProjectilePrefab;
        public NativeList<float2> Positions;
        public int Ammo;
        public float Cooldown;
    }
}
