using Unity.Collections;
using Unity.Mathematics;
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
    }
}
