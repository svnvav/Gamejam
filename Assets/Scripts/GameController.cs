
using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField] private PlanetsSpawner _planetsSpawner;
        
        private List<Planet> _planets;

        private void Awake()
        {
            Instance = this;
            _planets = new List<Planet>();
        }

        private void Update()
        {
            _planetsSpawner.GameUpdate();
            foreach (var planet in _planets)
            {
                planet.GameUpdate();
            }
        }

        public void AddPlanet(Planet planet)
        {
            _planets.Add(planet);
        }

        public void RemovePlanet(Planet planet)
        {
            _planets.Remove(planet);
            Destroy(planet.gameObject);
        }
    }
}
