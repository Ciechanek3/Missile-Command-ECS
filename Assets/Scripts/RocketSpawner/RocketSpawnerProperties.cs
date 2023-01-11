using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Rocket
{
    public struct RocketSpawnerProperties : IComponentData
    {
        public NativeList<float3> Targets;
        public float2 SpawnArea;
        public Entity RocketPrefab;
        public int SpawnPoolNumber;
        public float SpawnDelay;
    }
}
