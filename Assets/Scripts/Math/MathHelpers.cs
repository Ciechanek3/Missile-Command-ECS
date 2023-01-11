using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Math
{
    public static class MathHelpers
    {
        public static float3 TransformsToFloat3(Transform transform)
        {
            float3 convertedTransform = new float3(transform.position.x, transform.position.y, 0);
            return convertedTransform;
        }

        public static float GetDirection(float3 objectPosition, float3 targetPosition)
        {
            var x = objectPosition.x - targetPosition.x;
            var y = objectPosition.y - targetPosition.y;
            return math.atan2(x, y) + math.PI;
        }

        public static float3 TransformAspectToFloat3(TransformAspect transformAspect)
        {
            float3 convertedTransform = new float3(transformAspect.Position.x, transformAspect.Position.y, 0);
            return convertedTransform;
        }

        public static bool CheckIfFloatIsInArea(float3 objectPosition, float3 targetPosition, float2 offset)
        {
            if (objectPosition.x - targetPosition.x <= offset.x && objectPosition.x - targetPosition.x >= -offset.x)
            {
                if (objectPosition.y - targetPosition.y <= offset.y && objectPosition.y - targetPosition.y >= -offset.y )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
