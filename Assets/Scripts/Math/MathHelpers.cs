using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Math
{
    public static class MathHelpers
    {
        public static NativeArray<float2> TransformsToFloat2(Transform[] transforms)
        {
            NativeArray<float2> convertedTransforms = new NativeArray<float2>();
            for (int i = 0; i < transforms.Length; i++)
            {
                float2 convertedTransform = new float2(transforms[i].position.x, transforms[i].position.y);
                convertedTransforms[i] = convertedTransform;
            }

            return convertedTransforms;
        }

        public static float3 Float2ToFloat3(float2 f, float multiplier = 1f)
        {
            float3 f3 = new float3()
            {
                x = f.x * multiplier,
                y = f.y * multiplier,
                z = 0
            };

            return f3;
        }
    }
}
