using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Projectile
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        public GameObject explosionPrefab;
        public float movementSpeed;
    }

    public class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            AddComponent(new ProjectileProperties
            {
                ExplosionPrefab = GetEntity(authoring.explosionPrefab),
                MovementSpeed = authoring.movementSpeed,
                Destination = new float2(0, 0),
            });
        }
    }
}