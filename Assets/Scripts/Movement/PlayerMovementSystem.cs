using Marker;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Movement
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerMovementSystem : SystemBase
    {
        private PlayerMovement movement;

        protected override void OnUpdate()
        {
            if (movement == null)
            {
                movement = new PlayerMovement();
                movement.movement.Enable();
            }
            UnityEngine.Vector2 moveVector = movement.movement.Move.ReadValue<UnityEngine.Vector2>();
            float hAxis = moveVector.x;
            float vAxis = moveVector.y;
            float spaceKey = movement.movement.Shoot.ReadValue<float>();
            
            new MoveJob
            {
                Direction = new float2(hAxis, vAxis)
            }.Run();
        }
    }
    
    [BurstCompile]
    public partial struct MoveJob : IJobEntity
    {
        public float2 Direction;
        
        [BurstCompile]
        private void Execute(MarkerAspect marker)
        {
            marker.Move(Direction);
        }
    }
}
