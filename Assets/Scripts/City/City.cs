using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace City
{
    public class City : MonoBehaviour
    {
        [SerializeField] private Transform[] buildings;

        public static City Instance;   
        
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        public Transform GetRandomBuildingTransform()
        {
            return buildings[Random.Range(0, buildings.Length)];
        }
    }
}
