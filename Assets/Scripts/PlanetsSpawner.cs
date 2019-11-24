using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class PlanetsSpawner : MonoBehaviour
    {
        [SerializeField] private Planet _planetPrefab;
        [SerializeField] private float _startDelay;
        [SerializeField] private float _speed;
        [SerializeField] private float _speedIncreasing;
        [SerializeField] private float _cooldown;
        private float _progress;
        private bool _started;
        
        public void GameUpdate()
        {
            _progress += Time.deltaTime;
            if (!_started)
            {
                if (_progress >= _startDelay)
                {
                    _progress -= _startDelay;
                    _progress += _cooldown;
                    _started = true;
                }
                return;
            }

            while (_progress >= _cooldown)
            {
                _progress -= _cooldown;
                Spawn();
            }
        }

        private void Spawn()
        {
            var planet = Instantiate(_planetPrefab, transform.position, transform.rotation);
            planet.Speed = _speed;
            _speed += _speedIncreasing;
            GameController.Instance.AddPlanet(planet);
        }
    }
}