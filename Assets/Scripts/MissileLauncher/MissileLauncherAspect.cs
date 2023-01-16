using JetBrains.Annotations;
using Math;
using Movement;
using Projectile;
using Rocket;
using RocketSpawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MissileLauncher
{
    public readonly partial struct MissileLauncherAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;

        private readonly RefRW<MissileLauncherProperties> _missileLauncherProperties;
        private readonly RefRW<TargetPositionProperty> _targetPositionProperty;
        private readonly RefRW<MissileLauncherIndex> _missileLauncherIndex;
        private readonly RefRW<TimerData> _projectileSpawnTimer;

        private float2 FirePosition => _missileLauncherProperties.ValueRO.Position;

        private float2 TargetPosition
        {
            get => _targetPositionProperty.ValueRO.Position;
            set => _targetPositionProperty.ValueRW.Position = value;
        }

        public float Cooldown
        {
            get => _missileLauncherProperties.ValueRO.Cooldown;
        }
        public bool ShouldSpawnNewProjectile => ProjectileSpawnTimer <= 0f;
        
        public int Ammo
        {
            get => _missileLauncherProperties.ValueRO.Ammo;
            set => _missileLauncherProperties.ValueRW.Ammo = value;
        }

        public float ProjectileSpawnTimer
        {
            get => _projectileSpawnTimer.ValueRO.Timer;
            set => _projectileSpawnTimer.ValueRW.Timer = value;
        }

        public bool Fire()
        {
            if (Ammo <= 0)
            {
                return false;
            }

            Ammo--;
            return true;
        }

        public Entity ProjectileEntity => _missileLauncherProperties.ValueRO.ProjectilePrefab;
        
        public UniformScaleTransform GetMissileSpawnPoint()
        {
            float3 startingPosition = new float3(FirePosition.x, FirePosition.y, 0);
            float3 targetPosition = new float3(TargetPosition.x, TargetPosition.y, 0);
            float rotation = MathHelpers.GetDirection(startingPosition, targetPosition);
            return new UniformScaleTransform()
            {
                Position = startingPosition,
                Rotation = quaternion.RotateZ(-rotation),
                Scale = 0.2f
            };
        }

        public void SetTarget(float2 direction)
            {
                TargetPosition += new float2(direction.x, direction.y) * 0.05f;
                if (TargetPosition.x >= 8.5)
                {
                    TargetPosition = new float2(8.5f, TargetPosition.y);
                }
                if (TargetPosition.x <= -8.5)
                {
                    TargetPosition = new float2(-8.5f, TargetPosition.y);
                }
                if (TargetPosition.y <= -5)
                {
                    TargetPosition = new float2(TargetPosition.x, -5);
                }
                if (TargetPosition.y >= 5)
                {
                    TargetPosition = new float2(TargetPosition.x, 5);
                }
            }
            
    }
}