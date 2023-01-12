using MissileLauncher;
using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Math
{
    public class MissileLauncherAuthoring : MonoBehaviour
    {
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
                Cooldown = authoring.cooldown,
                InitialAmmo = authoring.initialAmmo
            });
        }
    }
}
