using Math;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace City
{
    public class CityMono : MonoBehaviour
    {
        public Transform[] buildings;
    }

    public class CityBaker : Baker<CityMono>
    {
        public override void Bake(CityMono authoring)
        {
            NativeArray<float2> coordinates = MathHelpers.TransformsToFloat2(authoring.buildings);
            
            AddComponent(new CityProperties
            {
                BuildingsPlaces = coordinates
            });
        }
    }
}
