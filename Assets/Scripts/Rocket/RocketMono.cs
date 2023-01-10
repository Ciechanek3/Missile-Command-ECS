using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RocketMono : MonoBehaviour
{
    public float defaultSpeed;
}

public class RocketBaker : Baker<RocketMono>
{
    public override void Bake(RocketMono authoring)
    {
        AddComponent(new RocketBehavior
        {
            Speed = authoring.defaultSpeed
        });
    }
}
