﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Plarium.Gamejam2019
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private float _speed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        [SerializeField] private List<RaceOnPlanet> _races;
        public List<RaceOnPlanet> Races => _races;

        [SerializeField] private Transform _leftRacePlaceholder;
        [SerializeField] private Transform _rightRacePlaceholder;
        [SerializeField] private GameObject _spritesMask;

        [SerializeField] private Vector3 _direction;

        [SerializeField] private Animator _animator;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _holyClips;
        [SerializeField] private AudioClip[] _fightClips;

        private bool _dead;
        
        public int RacesCount => _races.Count;

        private void Awake()
        {
            DetermineState();
        }

        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            _animator.Play("Planet");
        }
        
        public void GameUpdate()
        {
            transform.position += _speed * Time.deltaTime * _direction;
        }
        
        public void StepUpdate()
        {
            if (_races.Count == 1)
            {
                _races[0].AddPopulation(1);
            }

            if (_races.Count == 2)
            {
                if (_races[0].Race.IsAngry)
                {
                    _races[1].AddPopulation(-1);
                }
                else
                {
                    _races[1].AddPopulation(1);
                }
                if (_races[1].Race.IsAngry)
                {
                    _races[0].AddPopulation(-1);
                }
                else
                {
                    _races[0].AddPopulation(1);
                }
            }
        }

        public bool AddRace(RaceOnPlanet raceOnPlanet)
        {
            if (_races.Count >= 2) return false;
            
            _races.Add(raceOnPlanet);
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
                _spritesMask.SetActive(false);
                _races[0].transform.SetParent(_leftRacePlaceholder, false);
                _races[0].SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
            
            if (_races.Count == 2)
            {
                _spritesMask.SetActive(true);
                _races[0].transform.SetParent(_leftRacePlaceholder, false);
                _races[0].SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                _races[1].transform.SetParent(_rightRacePlaceholder, false);
                _races[1].SpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                if (_races[0].Race.IsAngry || _races[1].Race.IsAngry)
                {
                    var index = Random.Range(0, _fightClips.Length);
                    _audioSource.PlayOneShot(_fightClips[index]);
                }
                else
                {
                    var index = Random.Range(0, _holyClips.Length);
                    _audioSource.PlayOneShot(_holyClips[index]);
                }
            }
        }

        public void Die()
        {
            if(_dead) return;
            _animator.Play("Die");
            GameController.Instance.RemovePlanet(this);
            _dead = true;
        }
        
    }
}