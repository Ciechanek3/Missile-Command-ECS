using Unity.Entities;
using Unity.Mathematics;

namespace MissileLauncher
{
    public struct MissileLauncherProperties : IComponentData
    {
        public float2 FirePosition;
        public float Cooldown;
        public int InitialAmmo;
    }
}
