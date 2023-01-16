using MissileLauncher;
using Projectile;
using Rocket;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace MissileLauncher
{
    public class MissileLauncherAuthoring : MonoBehaviour
    {
        public Transform missileLauncherFirePosition;
        public GameObject projectilePrefab;
        public int initialAmmo;
        public float cooldown;
    }

    public class MissileLauncherBaker : Baker<MissileLauncherAuthoring>
    {
        public override void Bake(MissileLauncherAuthoring authoring)
        {
            var position = authoring.missileLauncherFirePosition.position;
            float2 positionValue = new float2(position.x, position.y);
            AddComponent(new MissileLauncherProperties
            {
                ProjectilePrefab = GetEntity(authoring.projectilePrefab),
                Position = positionValue,
                Ammo = authoring.initialAmmo,
                Cooldown = authoring.cooldown
            });
            
            AddComponent(new TargetPositionProperty
            {
                Position = new float2(0,0)
            });
            AddComponent<MissileLauncherIndex>();
            AddComponent<TimerData>();
        }
    }
}
