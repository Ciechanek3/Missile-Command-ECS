using Rocket;
using Unity.Entities;
using UnityEngine;

namespace Game
{
    public class GameAuthoring : MonoBehaviour
    {
        public GameObject gameOver;
    }

    public class GameBaker : Baker<GameAuthoring>
    {
        public override void Bake(GameAuthoring authoring)
        {
            AddComponent(new GameTag
            {
                GameOver = GetEntity(authoring.gameOver)
            });
            AddComponent(new TimerData
            {
                Timer = 0f
            });
        }
    }
}
