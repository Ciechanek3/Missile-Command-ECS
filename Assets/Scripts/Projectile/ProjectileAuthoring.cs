using Unity.Entities;
using UnityEngine;

namespace Projectile
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        public float movementSpeed;
        public GameObject explosionPrefab;
    }

    public class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            AddComponent(new ProjectileProperties
            {
                ExplosionPrefab = GetEntity(authoring.explosionPrefab),
                MovementSpeed = authoring.movementSpeed
            });
        }
    }
}