using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private float _speed;
        
        [SerializeField] private List<RaceOnPlanet> _races;
        public List<RaceOnPlanet> Races => _races;

        [SerializeField] private Transform _leftRacePlaceholder;
        [SerializeField] private Transform _rightRacePlaceholder;
        [SerializeField] private GameObject _spritesMask;

        [SerializeField] private Vector3 _direction;
        
        public int RacesCount => _races.Count;

        private void Awake()
        {
            DetermineState();
        }

        public void Initialize(Sprite sprite, Vector3 direction, float speed)
        {
            _races = new List<RaceOnPlanet>();
            _direction = direction;
            _speed = speed;
            _spriteRenderer.sprite = sprite;
        }
        
        public void GameUpdate()
        {
            transform.Translate(_speed * Time.deltaTime * _direction);
        }
        
        public void StepUpdate()
        {
            if (_races.Count == 1)
            {
                _races[0].AddPopulation(1);
            }
            
        }

        public bool AddRace(RaceOnPlanet raceOnPlanet)
        {
            if (_races.Count >= 2) return false;
            
            _races.Add(raceOnPlanet);
            raceOnPlanet.transform.SetParent(_races.Count == 1 ? _leftRacePlaceholder : _rightRacePlaceholder, false);
            DetermineState();
            return true;
        }

        public RaceOnPlanet GetRace(int index)
        {
            return _races[index];
        }

        public void RemoveRace(RaceOnPlanet raceOnPlanet)
        {
            _races.Remove(raceOnPlanet);
            DetermineState();
        }

        public void RemoveRace(int index)
        {
            _races.RemoveAt(index);
            DetermineState();
        }

        private void DetermineState()
        {
            if (_races.Count == 0)
            {
                _spritesMask.SetActive(false);
            }

            if (_races.Count == 1)
            {
                _spritesMask.SetActive(true);
            }
            
            if (_races.Count == 2)
            {
                _spritesMask.SetActive(false);
            }
        }

        public void Die()
        {
            GameController.Instance.RemovePlanet(this);
        }
    }
}