using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Rocket
{
    public class RocketSpawnerMono : MonoBehaviour
    {
        public float2 spawnArea;
        public float speed;
        public GameObject rocketPrefab;
        public int spawnPoolNumber;
        public uint randomSeed;
        public float spawnDelay;
    }
    
    public class RocketBaker : Baker<RocketSpawnerMono>
    {
        public override void Bake(RocketSpawnerMono authoring)
        {
            AddComponent(new RocketSpawnerProperties
            {
                SpawnArea = authoring.spawnArea,
                Speed = authoring.speed,
                RocketPrefab =  GetEntity(authoring.rocketPrefab),
                SpawnPoolNumber = authoring.spawnPoolNumber,
                SpawnDelay = authoring.spawnDelay
            });
            AddComponent(new RocketSpawnerRandom
            {
                Seed = Random.CreateFromIndex(authoring.randomSeed)
            });
            AddComponent<RocketSpawnTimer>();
        }
    }
}
