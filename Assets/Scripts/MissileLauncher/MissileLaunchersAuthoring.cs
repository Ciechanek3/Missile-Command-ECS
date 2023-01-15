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
        public MissileLauncherData[] missileLaunchersData;
        public GameObject projectilePrefab;
        public int initialAmmo;
        public float cooldown;
    }

    public class MissileLauncherBaker : Baker<MissileLauncherAuthoring>
    {
        public override void Bake(MissileLauncherAuthoring authoring)
        {
            NativeList<float2> missileLaunchersFirePositions = new NativeList<float2>();
            for (int i = 0; i < authoring.missileLaunchersData.Length; i++)
            {
                var position = authoring.missileLaunchersData[i].barrelTransform.position;
                missileLaunchersFirePositions.Add(new float2(
                    position.x,
                    position.y));
                
            }
            AddComponent(new MissileLauncherProperties
            {
                ProjectilePrefab = GetEntity(authoring.projectilePrefab),
                Positions = missileLaunchersFirePositions,
                Ammo = authoring.initialAmmo,
                Cooldown = authoring.cooldown
            });
            
            AddComponent(new TargetPositionProperty
            {
                Position = new float2(0,0)
            });
            AddComponent<MissileLauncherIndex>();
            AddComponent<ProjectileSpawnTimer>();
        }
    }

    [System.Serializable]
    public struct MissileLauncherData
    {
        public Transform barrelTransform;
    }
}
