using System.Collections.Generic;
using City;
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
        public float2 targetsOffset;
        public float2 spawnArea;
        public GameObject rocketPrefab;
        public float randomSeed;
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
                SpawnArea = authoring.spawnArea,
                RocketPrefab =  GetEntity(authoring.rocketPrefab),
                SpawnDelay = authoring.spawnDelay
            });
            float randomSeed = UnityEngine.Random.Range(0, authoring.randomSeed);
            AddComponent(new RocketSpawnerRandom
            {
                Seed = Random.CreateFromIndex((uint)randomSeed)
            });
            AddComponent<TimerData>();
            AddComponent(new TargetsData
            {
                Targets = targets,
                TargetsOffset = authoring.targetsOffset
            });
        }
    }
}
