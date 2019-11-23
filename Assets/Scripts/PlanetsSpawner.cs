using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class PlanetsSpawner : MonoBehaviour
    {
        [SerializeField] private Planet _planetPrefab;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private float _spawnRaduis;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _cooldown;
        private float _progress;
        
        public void GameUpdate()
        {
            _progress += Time.deltaTime;
            while (_progress >= _cooldown)
            {
                _progress -= _cooldown;
                Spawn();
            }
        }

        private void Spawn()
        {
            var sprite = _sprites[Random.Range(0, _sprites.Length)];
            var speed = Random.Range(_minSpeed, _maxSpeed);
            var position = Random.insideUnitCircle.normalized * _spawnRaduis;
            var planet = Instantiate(_planetPrefab, position, Quaternion.identity);
            var direction = -position.normalized;
            //planet.Initialize(sprite, direction, speed);
            GameController.Instance.AddPlanet(planet);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _spawnRaduis);
        }
    }
}