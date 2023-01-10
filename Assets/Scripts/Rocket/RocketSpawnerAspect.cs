using System;
using City;
using Math;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Rocket.Aspect
{
    public readonly partial struct RocketSpawnerAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<RocketSpawnerProperties> _rocketSpawnerProperties;
        private readonly RefRW<RocketSpawnerRandom> _rocketSpawnerRandom;
        private readonly RefRW<RocketSpawnTimer> _rocketSpawnTimer;

        public float SpawnDelay => _rocketSpawnerProperties.ValueRO.SpawnDelay;
        public int SpawnPoolNumber => _rocketSpawnerProperties.ValueRO.SpawnPoolNumber;

        public float RocketSpawnTimer
        {
            get => _rocketSpawnTimer.ValueRO.Timer;
            set => _rocketSpawnTimer.ValueRW.Timer = value;
        }
        
        public bool ShouldSpawnNewRocket => RocketSpawnTimer <= 0f;
        public Entity RocketPrefab => _rocketSpawnerProperties.ValueRO.RocketPrefab;
        //public 
        
        
        private float3 HalfSpawnArea => new()
        {
            x = _rocketSpawnerProperties.ValueRO.SpawnArea.x * 0.5f,
            y = _rocketSpawnerProperties.ValueRO.SpawnArea.y * 0.5f,
            z = 0f
        };
        
        private float3 SpawnAreaMinCorner => _transformAspect.Position - HalfSpawnArea;
        private float3 SpawnAreaMaxCorner => _transformAspect.Position + HalfSpawnArea;
        
        public float3 GetRandomRocketSpawnPoint()
        {
            float3 randomPosition = _rocketSpawnerRandom.ValueRW.Seed.NextFloat3(SpawnAreaMinCorner, SpawnAreaMaxCorner);

            return randomPosition;
        }

        public float3 GetRandomRocketDestination()
        {
            float3 destination =
                MathHelpers.TransformsToFloat3(
                    City.City.Instance.GetRandomBuildingTransform());

            return destination;
        }

        public UniformScaleTransform GetRandomRocketSpawn()
        {
            var startingPosition = GetRandomRocketSpawnPoint();
            return new UniformScaleTransform()
            {
                Position = GetRandomRocketSpawnPoint(),
                Rotation = quaternion.RotateZ(MathHelpers.GetDirection(GetRandomRocketSpawnPoint(),GetRandomRocketDestination())),
                Scale = 1f
            };
        }
    }
}
