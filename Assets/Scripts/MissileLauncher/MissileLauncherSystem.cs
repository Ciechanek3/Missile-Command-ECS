using System.Collections.Generic;
using Marker;
using Math;
using Movement;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace MissileLauncher
{
    
    [UpdateAfter(typeof(MarkerSystem))]
    public partial class MissileLauncherSystem : SystemBase
    {
        private bool _isDataCreated = false;
        private MissileLauncherAspect currentMissileLauncher;
        private MarkerAspect _markerAspect;
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(World.Unmanaged);
            if (_isDataCreated == false)
            {
                _isDataCreated = true;
                ChangeActiveLauncher(ecb, 1);
                Entity markerProperties = SystemAPI.GetSingletonEntity<MarkerProperties>();
                _markerAspect = SystemAPI.GetAspectRW<MarkerAspect>(markerProperties);
            }

            List<MissileLauncherAspect> tempMissileLaunchers = new List<MissileLauncherAspect>();
            foreach (var missileLauncher in SystemAPI.Query<MissileLauncherAspect>().WithAll<ActiveLauncherTag>())
            {
                tempMissileLaunchers.Add(missileLauncher);
                
                missileLauncher.FireProjectile(ecb, deltaTime);
            }
            tempMissileLaunchers.Clear();
            
            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);
            
            new MoveTargetJob
            {
                Direction = inputAspect.Movement
            }.Run();
        }
        
        [BurstCompile]
        public void ChangeActiveLauncher(EntityCommandBuffer ecbSingleton,int index)
        {
            List<MissileLauncherAspect> missileLauncherAspects = new List<MissileLauncherAspect>();
            foreach (var missileLauncher in SystemAPI.Query<MissileLauncherAspect>())
            {
                ecbSingleton.RemoveComponent<ActiveLauncherTag>(missileLauncher.Entity);
                missileLauncherAspects.Add(missileLauncher);
            }
                
            ecbSingleton.AddComponent<ActiveLauncherTag>(missileLauncherAspects[index].Entity);
        }
    }
    
    /*[BurstCompile]
    public partial struct FireProjectile : IJobEntity
    {
        public float DeltaTime;
        public float ProjectileSpawnTimer;
        public bool ShouldSpawnNewProjectile;
        public bool CanFire;
        public bool Cooldown;
        public MissileLauncherAspect MissileLauncher;
        public EntityCommandBuffer Ecb;
        public UniformScaleTransform USTransform;
        
        [BurstCompile]
        private void Execute()
        {
            MissileLauncher.ProjectileSpawnTimer -= DeltaTime;
            if (MissileLauncher.ShouldSpawnNewProjectile == false) return;
            if (MissileLauncher.Fire() == false) return;
            MissileLauncher.ProjectileSpawnTimer = MissileLauncher.Cooldown;
            var newRocket = Ecb.Instantiate(MissileLauncher.ProjectileEntity);
            Ecb.SetComponent(newRocket, new LocalToWorldTransform{ Value = USTransform});
            Ecb.Playback(new EntityManager());
        }
    }*/
    
    
    
}
