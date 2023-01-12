using System.Collections;
using System.Collections.Generic;
using Rocket;
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
        AddComponent(new RocketProperties
        {
            Speed = authoring.defaultSpeed
        });
    }
}
