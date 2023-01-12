using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Marker
{
    public readonly partial struct MarkerAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;

        private readonly RefRO<MarkerProperties> _markerProperties;

        private float GetMovementSpeed => _markerProperties.ValueRO.MovementSpeed;

        public void Move(Vector2 direction)
        {
            TransformAspect.Position += new float3(direction.x, direction.y, 0) * GetMovementSpeed;
            if (TransformAspect.Position.x >= 8.5)
            {
                TransformAspect.Position = new float3(8.5f, TransformAspect.Position.y, 0);
            }
            if (TransformAspect.Position.x <= -8.5)
            {
                TransformAspect.Position = new float3(-8.5f, TransformAspect.Position.y, 0);
            }
            if (TransformAspect.Position.y <= -5)
            {
                TransformAspect.Position = new float3(TransformAspect.Position.x, -5, 0);
            }
            if (TransformAspect.Position.y >= 5)
            {
                TransformAspect.Position = new float3(TransformAspect.Position.x, 5, 0);
            }
        }
    }
}
