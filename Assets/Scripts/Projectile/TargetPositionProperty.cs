using Unity.Entities;
using Unity.Mathematics;

namespace Projectile
{
    public struct TargetPositionProperty : IComponentData
    {
        public float2 Position;
    }
}
