using System.Collections.Generic;
using Marker;
using Math;
using Movement;
using Rocket;
using RocketSpawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace MissileLauncher
{

    [UpdateAfter(typeof(MarkerSystem))]
    public partial struct MissileLauncherSystem : ISystem
    {
        private bool _isDataCreated;
        private MissileLauncherAspect _missileLauncherAspect;
        private MarkerAspect _markerAspect;
        private float _time;

        public void OnCreate(ref SystemState state)
        {
            
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);
            _time += SystemAPI.Time.DeltaTime;

            new MoveTargetJob
            {
                Direction = inputAspect.Movement
            }.Run();
            
            bool mlpExist = SystemAPI.TryGetSingletonEntity<MissileLauncherProperties>(out Entity mlp);
            if (mlpExist)
            {
                _missileLauncherAspect = SystemAPI.GetAspectRW<MissileLauncherAspect>(mlp);
            }
            else
            {
                return;
            }

            if (inputAspect.Shooting != 0)
            {
                new FireProjectile
                {
                    DeltaTime = _time,
                    Ecb = ecb,
                    USTransform = _missileLauncherAspect.GetMissileSpawnPoint()
                }.Run();
                _time = 0;
            }

        }
    }

    public partial struct FireProjectile : IJobEntity
    {
        
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;

        private void Execute(MissileLauncherAspect missileLauncherAspect)
        {
            missileLauncherAspect.ProjectileSpawnTimer -= DeltaTime;
            if (missileLauncherAspect.ShouldSpawnNewProjectile == false) return;
            if (missileLauncherAspect.Fire() == false) return;
            missileLauncherAspect.ProjectileSpawnTimer = missileLauncherAspect.Cooldown;
            var newRocket = Ecb.Instantiate(missileLauncherAspect.ProjectileEntity);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform { Value = USTransform });
        }
    }
}

