using City;
using Marker;
using Movement;
using Rocket;
using RocketSpawner;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Game
{
    [BurstCompile]
    public partial struct GameSystem : ISystem
    {
        private bool _gameFinished;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Entity gt = SystemAPI.GetSingletonEntity<GameTag>();
            var game = SystemAPI.GetAspectRW<GameAspect>(gt);
            
            float time = SystemAPI.Time.DeltaTime;
            
            game.GameTimer += time;
            
            foreach (var target in SystemAPI.Query<TargetAspect>())
            {
                return;
            }
            
            if (!_gameFinished)
            {
                _gameFinished = true;
                
                var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
                var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
                Entity rsp = SystemAPI.GetSingletonEntity<RocketSpawnerProperties>();
                var rocketSpawner = SystemAPI.GetAspectRW<RocketSpawnerAspect>(rsp);

                foreach (var rocket in SystemAPI.Query<RocketAspect>())
                {
                    ecb.DestroyEntity(rocket.Entity);
                }
                
                rocketSpawner.ProjectileSpawnTimer = 10000f;
                new ShowGameOver
                {
                    Ecb = ecb,
                }.Run();
            }
        }
    }
    
    [BurstCompile]
    public partial struct ShowGameOver : IJobEntity
    {
        public EntityCommandBuffer Ecb;

        [BurstCompile]
        private void Execute(GameAspect gameAspect)
        {
            Ecb.Instantiate(gameAspect.GameOverEntity);
        }
    }
}
