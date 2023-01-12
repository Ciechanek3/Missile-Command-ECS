using MissileLauncher;
using Rocket;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Math
{
    public class MissileLauncherAuthoring : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform firePosition;
        public float cooldown;
        public int initialAmmo;
    }

    public class MissileLauncherBaker : Baker<MissileLauncherAuthoring>
    {
        public override void Bake(MissileLauncherAuthoring authoring)
        {
            var position = authoring.firePosition.position;
            AddComponent(new MissileLauncherProperties
            {
                FirePosition = new float2(position.x, position.y),
                ProjectilePrefab = GetEntity(authoring.projectilePrefab),
                Cooldown = authoring.cooldown,
            });
            AddComponent(new ProjectileSpawnTimer
            {
                Timer = authoring.cooldown
            });
            AddComponent(new AmmoCounter
            {
                Ammo = authoring.initialAmmo
            });
        }
    }
}
