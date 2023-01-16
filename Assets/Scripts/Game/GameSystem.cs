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
    public partial struct GameSystem : ISystem
    {
        private bool _gameFinished;
        public void OnCreate(ref SystemState state)
        {
        
        }

        public void OnDestroy(ref SystemState state)
        {
        
        }

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
                
                Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
                var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);
            }
        }
    }
    
    public partial struct ShowGameOver : IJobEntity
    {
        public EntityCommandBuffer Ecb;

        private void Execute(GameAspect gameAspect)
        {
            Ecb.Instantiate(gameAspect.GameOverEntity);
        }
    }
}
