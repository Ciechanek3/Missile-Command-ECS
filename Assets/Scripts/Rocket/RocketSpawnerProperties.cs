using Unity.Entities;
using Unity.Mathematics;

namespace Rocket
{
    public struct RocketSpawnerProperties : IComponentData
    {
        public float2 SpawnArea;
        public float Speed;
        public Entity RocketPrefab;
        public int SpawnPoolNumber;
        public float SpawnDelay;
    }
}
