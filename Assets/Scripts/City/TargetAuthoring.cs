using System.Collections;
using System.Collections.Generic;
using City;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TargetAuthoring : MonoBehaviour
{
    public float2 offset;
}

public class TargetBaker : Baker<TargetAuthoring>
{
    public override void Bake(TargetAuthoring authoring)
    {
        AddComponent(new TargetTag
        {
            Position = authoring.gameObject.transform.position,
            Offset = authoring.offset
        });
    }
}
