using System.Collections;
using System.Collections.Generic;
using Rocket;
using Unity.Entities;
using UnityEngine;

namespace Explosion
{
    public class ExplosionAuthoring : MonoBehaviour
    {
        public float explosionLifeTime;
    }

    public class ExplosionBaker : Baker<ExplosionAuthoring>
    {
        public override void Bake(ExplosionAuthoring authoring)
        {
            AddComponent<ExplosionTag>();
            AddComponent(new TimerData
            {
                Timer = authoring.explosionLifeTime
            });
        }
    }
}