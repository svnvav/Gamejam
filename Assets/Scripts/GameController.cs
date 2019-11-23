
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
        [SerializeField] private Scenario _scenario;
        [SerializeField] private float _gameSpeed = 1f;
        
        private float _scenarioProgress;
        private int _currentScenarioItemIndex;

        [SerializeField] private List<Planet> _planets;

        private float _stepProgress = 0f;
        private Planet _source;
        private int _raceIndexOnSourcePlanet;
        private Planet _destination;
        
        private List<RaceOnPlanet> _races;
        public List<RaceOnPlanet> Races => _races;
        
        private void Awake()
        {
            Instance = this;
            _currentScenarioItemIndex = 0;
        }

        private void Update()
        {
            _scenarioProgress += Time.deltaTime * _gameSpeed;
            while (_scenarioProgress > _scenario.Items[_currentScenarioItemIndex].delay)
            {
                _scenarioProgress -= _scenario.Items[_currentScenarioItemIndex].delay;
                AddPlanet(_scenario.Items[_currentScenarioItemIndex].planet);
            }
            
            _stepProgress += Time.deltaTime * _gameSpeed;
            while (_stepProgress > 1f)
            {
                _stepProgress -= 1f;
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
            foreach (var raceOnPlanet in planet.Races)
            {
                _hud.AddRace(raceOnPlanet);
            }
        }

        public void AddRace(RaceOnPlanet raceOnPlanet)
        {
            _races.Add(raceOnPlanet);
        }

        public void AddPlanetWithRaces(Planet planet, RaceOnPlanet raceOnPlanet)
        {
            
        }

        public void RemovePlanet(Planet planet)
        {
            _planets.Remove(planet);
            Destroy(planet.gameObject);
        }
    }
}
