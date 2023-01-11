using Unity.Entities;
using Unity.Transforms;

namespace City
{
    public readonly partial struct TargetAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;
        
        private readonly RefRO<TargetTag> _targetTag;
    }
}
