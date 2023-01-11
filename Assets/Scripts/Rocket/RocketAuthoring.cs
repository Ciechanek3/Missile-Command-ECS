using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RocketAuthoring : MonoBehaviour
{
    public float defaultSpeed;
}

public class RocketBaker : Baker<RocketAuthoring>
{
    public override void Bake(RocketAuthoring authoring)
    {
        AddComponent(new RocketBehavior
        {
            Speed = authoring.defaultSpeed
        });
    }
}