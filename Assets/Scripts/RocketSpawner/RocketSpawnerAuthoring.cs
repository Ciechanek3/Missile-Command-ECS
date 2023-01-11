using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Rocket
{
    public class RocketSpawnerAuthoring : MonoBehaviour
    {
        public List<float2> targets;
        public float2 spawnArea;
        public GameObject rocketPrefab;
        public int spawnPoolNumber;
        public uint randomSeed;
        public float spawnDelay;
    }
    
    public class RocketSpawnerBaker : Baker<RocketSpawnerAuthoring>
    {
        public override void Bake(RocketSpawnerAuthoring authoring)
        {
            AddComponent(new RocketSpawnerProperties
            {
                Targets = authoring.targets.ToNativeList(Allocator.Temp),
                SpawnArea = authoring.spawnArea,
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
