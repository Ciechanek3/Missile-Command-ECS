using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Marker
{
    public class MarkerAuthoring : MonoBehaviour
    {
        public float movementSpeed;
    }

    public class MarkerBaker : Baker<MarkerAuthoring>
    {
        public override void Bake(MarkerAuthoring authoring)
        {
            AddComponent<MarkerProperties>();
        }
    }
}