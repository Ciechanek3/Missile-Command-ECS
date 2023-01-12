using System;
using Marker;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Movement
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class PlayerMovementSystem : SystemBase
    {
        private PlayerMovement _movement;

        protected override void OnCreate()
        {
            EntityManager.AddComponent<InputData>(SystemHandle);
        }
        [BurstCompile]
        protected override void OnUpdate()
        {
            if (_movement == null)
            {
                _movement = new PlayerMovement();
                _movement.movement.Enable();
            }

            
            UnityEngine.Vector2 moveVector = _movement.movement.Move.ReadValue<UnityEngine.Vector2>();
            float hAxis = moveVector.x;
            float vAxis = moveVector.y;
            float spaceKey = _movement.movement.Shoot.ReadValue<float>();

            Entity inputData = SystemAPI.GetSingletonEntity<InputData>();
            var inputAspect = SystemAPI.GetAspectRW<InputAspect>(inputData);

            inputAspect.ModifyMovement(new float2(moveVector.x, moveVector.y));
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
