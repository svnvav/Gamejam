using UnityEngine;

namespace Plarium.Gamejam2019
{
    
    public class Scenario : MonoBehaviour
    {
        [System.Serializable]
        public struct ScenarioItem
        {
            public Planet planet;
            public float delay;
        }

        [SerializeField] private ScenarioItem[] _items;

        public ScenarioItem[] Items => _items;
    }
}