using Rocket;
using Unity.Entities;
using Unity.Transforms;

namespace Game
{
    public readonly partial struct GameAspect : IAspect
    {
        private readonly Entity _entity;
        private readonly TransformAspect _transformAspect;

        private readonly RefRO<GameTag> _gameTag;
        private readonly RefRW<TimerData> _gameTimer;

        public float GameTimer
        {
            get => _gameTimer.ValueRO.Timer;
            set => _gameTimer.ValueRW.Timer = value;
        }

        public Entity GameOverEntity
        {
            get => _gameTag.ValueRO.GameOver;
        }
    }
}
