using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Rocket
{
    public struct RocketSpawnerProperties : IComponentData
    {
        public float2 SpawnArea;
        public Entity RocketPrefab;
        public float SpawnDelay;
    }
}
