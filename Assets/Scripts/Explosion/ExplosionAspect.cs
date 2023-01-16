using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Explosion
{
    public readonly partial struct ExplosionAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly TransformAspect _transformAspect;
        public readonly RefRO<ExplosionData> _explosionData;
    }
}
