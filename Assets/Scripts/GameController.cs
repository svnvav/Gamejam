
using System;
using System.Collections;
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
        [SerializeField] private GameObject _blackHole;
        
        private float _scenarioProgress;
        private int _currentScenarioItemIndex;

        [SerializeField] private List<Planet> _planets;

        private float _stepProgress = 0f;
        private Planet _source;
        private int _raceIndexOnSourcePlanet;
        private Planet _destination;

        private int _starsLeft;

        private bool _paused;
        private bool _began;
        
        private List<RaceOnPlanet> _races;
        public List<RaceOnPlanet> Races => _races;
        
        private void Awake()
        {
            _races = new List<RaceOnPlanet>();
            Instance = this;
            _currentScenarioItemIndex = 0;
            _starsLeft = 4;
        }

        private void Start()
        {
            //_hud.ShowStartPanel();
            Begin();
        }

        public void Begin()
        {
            _began = true;
            _blackHole.SetActive(true);
        }

        public void OnPause()
        {
            _paused = !_paused;
            Time.timeScale = _paused ? 0f : 1f;
        }

        private void Update()
        {
            if(!_began) return;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause();
                _hud.Pause(_paused);
            }
            
            if (_currentScenarioItemIndex < _scenario.Items.Length)
            {
                _scenarioProgress += Time.deltaTime * _gameSpeed;
                while (_currentScenarioItemIndex < _scenario.Items.Length && _scenarioProgress > _scenario.Items[_currentScenarioItemIndex].delay)
                {
                    _scenarioProgress -= _scenario.Items[_currentScenarioItemIndex].delay;
                    AddPlanet(_scenario.Items[_currentScenarioItemIndex].planet);
                    _currentScenarioItemIndex++;
                }
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

            //_planetsSpawner.GameUpdate();
            foreach (var planet in _planets)
            {
                planet.GameUpdate();
            }

            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(TouchPos, Vector2.zero);
                if (hit.collider != null)
                {
                    var dragger = hit.collider.GetComponent<Dragger>();
                    CapturePlanet(dragger.Planet, dragger.Index);
                }
            } else if (Input.GetMouseButtonUp(0))
            {
                var hit = Physics2D.Raycast(TouchPos, Vector2.zero);
                if (hit.collider != null)
                {
                    var dragger = hit.collider.GetComponent<Dragger>();
                    if (dragger != null)
                    {
                        _destination = dragger.Planet;
                        DeliverRace();
                    }
                }
                ClearPath();
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
            if(_source == null) return;
            var race = _source.GetRace(_raceIndexOnSourcePlanet);
            _source.RemoveRace(_raceIndexOnSourcePlanet);
            _destination.AddRace(race);
        }
        
        public void AddPlanet(Planet planet)
        {
            _planets.Add(planet);
            planet.Initialize(-planet.transform.position.normalized);
            foreach (var raceOnPlanet in planet.Races)
            {
                AddRace(raceOnPlanet);
            }
        }

        public void AddRace(RaceOnPlanet raceOnPlanet)
        {
            _races.Add(raceOnPlanet);
            _began = false;
            _hud.AddRace(raceOnPlanet);
        }

        public void RemovePlanet(Planet planet)
        {
            
            StartCoroutine(DalayedPlanetDestroy(planet, 0.5f, 0.2f));
        }

        private IEnumerator DalayedPlanetDestroy(Planet planet, float delay1, float delay2)
        {
            var expired = delay1;
            
            while (expired > 0f)
            {
                expired -= Time.deltaTime;
                yield return null;
            }

            if (_source != null)
            {
                ClearPath();
            }
            foreach (var raceOnPlanet in planet.Races)
            {
                raceOnPlanet.Die();
            }
            
            _planets.Remove(planet);
            
            
            expired = delay2;
            while (expired > 0f)
            {
                expired -= Time.deltaTime;
                yield return null;
            }
            
            Destroy(planet.gameObject);
        }
        
        public void RemoveRace(RaceOnPlanet raceOnPlanet)
        {
            _hud.RemoveRace(raceOnPlanet, _starsLeft);
            _races.Remove(raceOnPlanet);
            _starsLeft--;
            if (_starsLeft < 0)
            {
                Lose();
            }
        }

        public void Lose()
        {
            
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
