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

        [BurstCompile]
        public void ChangeActiveLauncher(EntityCommandBuffer ecbSingleton, int index)
        {
            List<MissileLauncherAspect> missileLauncherAspects = new List<MissileLauncherAspect>();
            /*foreach (var missileLauncher in SystemAPI.Query<MissileLauncherAspect>())
            {
                ecbSingleton.RemoveComponent<ActiveLauncherTag>(missileLauncher.Entity);
                missileLauncherAspects.Add(missileLauncher);
            }
                
            ecbSingleton.AddComponent<ActiveLauncherTag>(missileLauncherAspects[index].Entity);*/
        }

        public void OnCreate(ref SystemState state)
        {
            Entity mlp = SystemAPI.GetSingletonEntity<MissileLauncherProperties>();
            _missileLauncherAspect = SystemAPI.GetAspectRW<MissileLauncherAspect>(mlp);
            _missileLauncherAspect.ChangeCurrentLauncher(1);
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {


            /*if (_isDataCreated == false)
            {
                _isDataCreated = true;
                ChangeActiveLauncher(ecb, 1);
                Entity markerProperties = SystemAPI.GetSingletonEntity<MarkerProperties>();
                _markerAspect = SystemAPI.GetAspectRW<MarkerAspect>(markerProperties);
            }*/

            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);
            var deltaTime = SystemAPI.Time.DeltaTime;

            new MoveTargetJob
            {
                Direction = inputAspect.Movement
            }.Run();


            if (inputAspect.Shooting != 0)
            {
                new FireProjectile
                {
                    DeltaTime = deltaTime,
                    Ecb = ecb,
                    USTransform = _missileLauncherAspect.GetMissileSpawnPoint()
                }.Run();
            }

        }
    }

    public partial struct FireProjectile : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;
        public float2 Target;

        [BurstCompile]
        private void Execute(MissileLauncherAspect missileLauncherAspect)
        {
            //missileLauncherAspect.ProjectileSpawnTimer -= DeltaTime;
            //if (missileLauncherAspect.ShouldSpawnNewProjectile == false) return;
            //if (missileLauncherAspect.Fire() == false) return;
            //missileLauncherAspect.ProjectileSpawnTimer = missileLauncherAspect.Cooldown;
            var newRocket = Ecb.Instantiate(missileLauncherAspect.ProjectileEntity);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform { Value = USTransform });
            //Ecb.SetComponent(newRocket);
            //Ecb.Playback(new EntityManager());

        }
    }

    [BurstCompile]
    public partial struct SpawnProjectileJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;

        [BurstCompile]
        private void Execute(MissileLauncherAspect missileLauncherAspect)
        {
            Debug.Log("2");
            missileLauncherAspect.ProjectileSpawnTimer -= DeltaTime;
            if (missileLauncherAspect.ShouldSpawnNewProjectile == false) return;
            if (missileLauncherAspect.Fire() == false) return;
            missileLauncherAspect.ProjectileSpawnTimer = missileLauncherAspect.Cooldown;
            var newRocket = Ecb.Instantiate(missileLauncherAspect.ProjectileEntity);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform { Value = USTransform });
        }
    }

}

