
using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        private Vector2 TouchPos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        [SerializeField] private HUD _hud;
        [SerializeField] private PlanetsSpawner _planetsSpawner;
        [SerializeField] private LineRenderer _path;

        [SerializeField] private float _gameSpeed = 1f;
        private float _stepProgress = 0f;

        private List<RaceOnPlanet> _races;
        public List<RaceOnPlanet> Races => _races;

        [SerializeField] private List<Planet> _planets;

        private Planet _source;
        private int _raceIndexOnSourcePlanet;
        private Planet _destination;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _stepProgress += Time.deltaTime;
            while (_stepProgress > _gameSpeed)
            {
                _stepProgress -= _gameSpeed;
                foreach (var planet in _planets)
                {
                    planet.StepUpdate();
                }
                _hud.StepUpdate();
            }
            
            _planetsSpawner.GameUpdate();
            foreach (var planet in _planets)
            {
                planet.GameUpdate();
            }

            if (Input.GetMouseButtonUp(1))
            {
                ClearPath();
            }

            if (_source != null)
            {
                _path.SetPosition(0, _source.transform.position);
                _path.SetPosition(1, TouchPos);
            }
        }

        public void ClearPath()
        {
            _source = null;
            _raceIndexOnSourcePlanet = 0;
            _path.gameObject.SetActive(false);
        }
        
        public void OnPlanetClick(Planet planet, int raceIndex)
        {
            if (_source == null)
            {
                CapturePlanet(planet, raceIndex);
            }
            else
            {
                _destination = planet;
                DeliverRace();
                ClearPath();
            }
        }

        private void CapturePlanet(Planet planet, int raceIndex)
        {
            if(planet.RacesCount <= 0) return;
            _source = planet;
            _raceIndexOnSourcePlanet = planet.RacesCount <= raceIndex ? 0 : raceIndex;
            _path.gameObject.SetActive(true);
            _path.transform.position = planet.transform.position;
        }

        public void DeliverRace()
        {
            var race = _source.GetRace(_raceIndexOnSourcePlanet);
            _source.RemoveRace(_raceIndexOnSourcePlanet);
            _destination.AddRace(race);
        }
        
        public void AddPlanet(Planet planet)
        {
            _planets.Add(planet);
        }

        public void AddPlanetWithRace(Planet planet, RaceOnPlanet raceOnPlanet)
        {
            
        }

        public void RemovePlanet(Planet planet)
        {
            _planets.Remove(planet);
            Destroy(planet.gameObject);
        }
    }
}
