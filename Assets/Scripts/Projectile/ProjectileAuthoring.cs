using System.Collections;
using System.Collections.Generic;
using Projectile;
using Rocket;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float movementSpeed;
}

public class ProjectileBaker : Baker<ProjectileAuthoring>
{
    public override void Bake(ProjectileAuthoring authoring)
    {
        AddComponent(new ProjectileProperties
        {
            MovementSpeed = authoring.movementSpeed
        });
    }
}
