using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace City
{
    public class TargetAuthoring : MonoBehaviour
    {
        public float2 offset;
    }

    public class TargetBaker : Baker<TargetAuthoring>
    {
        public override void Bake(TargetAuthoring authoring)
        {
            AddComponent<TargetTag>();
        }
    }
}