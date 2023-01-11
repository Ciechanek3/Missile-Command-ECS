using System.Collections.Generic;
using Math;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Rocket
{
    public class RocketSpawnerAuthoring : MonoBehaviour
    {
        public List<Transform> targets;
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
            NativeList<float3> targets = new NativeList<float3>(Allocator.Persistent);
            for (int i = 0; i < authoring.targets.Count; i++)
            {
                targets.Add(MathHelpers.TransformsToFloat3(authoring.targets[i]));
            }
            AddComponent(new RocketSpawnerProperties
            {
                Targets = targets,
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
