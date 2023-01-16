using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Explosion
{
    public class ExplosionAuthoring : MonoBehaviour
    {
    
    }

    public class ExplosionBaker : Baker<ExplosionAuthoring>
    {
        public override void Bake(ExplosionAuthoring authoring)
        {
            AddComponent<ExplosionData>();
        }
    }
}