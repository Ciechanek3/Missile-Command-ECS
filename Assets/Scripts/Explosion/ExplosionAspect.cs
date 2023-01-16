using Rocket;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Explosion
{
    public readonly partial struct ExplosionAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;
        
        private readonly RefRO<ExplosionTag> _explosionData;
        private readonly RefRW<TimerData> _lastingDuration;
        
        public float LastingDuration
        {
            get => _lastingDuration.ValueRO.Timer;
            set => _lastingDuration.ValueRW.Timer = value;
        }
        public bool ShouldDestroy => LastingDuration <= 0f;
    }
}
