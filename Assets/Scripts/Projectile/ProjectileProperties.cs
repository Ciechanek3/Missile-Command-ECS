using Unity.Entities;
using Unity.Mathematics;

namespace Projectile
{
    public struct ProjectileProperties : IComponentData
    {
        public float MovementSpeed;
        public float2 Destination;
    }
}
