using JetBrains.Annotations;
using Movement;
using Projectile;
using Rocket;
using RocketSpawner;
using Unity.Burst;
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

        private readonly RefRO<MissileLauncherProperties> _missileLauncherProperties;
        private readonly RefRW<ProjectileSpawnTimer> _projectileSpawnTimer;
        private readonly RefRW<TargetPositionProperty> _targetPositionProperty;
        private readonly RefRW<AmmoCounter> _ammoCounter;

        private float2 FirePosition => _missileLauncherProperties.ValueRO.FirePosition;

        private float2 TargetPosition
        {
            get => _targetPositionProperty.ValueRO.Position;
            set => _targetPositionProperty.ValueRW.Position = value;
        }
        public float ProjectileSpawnTimer
        {
            get => _projectileSpawnTimer.ValueRO.Timer;
            set => _projectileSpawnTimer.ValueRW.Timer = value;
        }
        
        public bool ShouldSpawnNewProjectile => ProjectileSpawnTimer <= 0f;

        
        public float Cooldown => _missileLauncherProperties.ValueRO.Cooldown;
        public int Ammo
        {
            get => _ammoCounter.ValueRO.Ammo;
            set => _ammoCounter.ValueRW.Ammo = value;
        }

        public bool Fire()
        {
            if (Ammo <= 0)
            {
                return false;
            }
            Ammo -= 1;
            return true;
        }

        public void FireProjectile(EntityCommandBuffer ecb, float deltaTime, InputAspect inputAspect)
        {
            ProjectileSpawnTimer -= deltaTime;
            if (ShouldSpawnNewProjectile == false) return;
            if (inputAspect.Shooting == 0) return;
            if (Fire() == false) return;
            ProjectileSpawnTimer = Cooldown;
            var newProjectile = ecb.Instantiate(ProjectileEntity);
            ecb.SetComponent(newProjectile, new LocalToWorldTransform{ Value = GetMissileSpawnPoint()});
            ecb.AddComponent(newProjectile, new TargetPositionProperty
            {
                Position = _targetPositionProperty.ValueRW.Position
            });
            
        }

        public Entity ProjectileEntity => _missileLauncherProperties.ValueRO.ProjectilePrefab;
        
        public UniformScaleTransform GetMissileSpawnPoint()
        {
            float3 startingPosition = new float3(FirePosition.x, FirePosition.y, 0);
            return new UniformScaleTransform()
            {
                Position = startingPosition,
                Rotation = quaternion.identity,
                Scale = 1f
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