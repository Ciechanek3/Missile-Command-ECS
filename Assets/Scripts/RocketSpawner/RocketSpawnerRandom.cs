using Unity.Entities;
using Unity.Mathematics;

namespace Rocket
{
    public struct RocketSpawnerRandom : IComponentData
    {
        public Random Seed;
    }
}
