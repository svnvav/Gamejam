using System.Collections.Generic;
using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private float _speed;
        
        [SerializeField] private List<RaceOnPlanet> _races;

        private Vector3 _direction;
        
        public int RacesCount => _races.Count;

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

        public void Die()
        {
            GameController.Instance.RemovePlanet(this);
        }
    }
}