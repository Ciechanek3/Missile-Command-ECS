using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace City
{
    public struct CityProperties : IComponentData
    {
        public NativeArray<float2> BuildingsPlaces;
    }
}
