using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class RaceOnPlanet : MonoBehaviour
    {
        [SerializeField] private Race _race;
        public Race Race => _race;

        [SerializeField] private int _population;
        public int Population => _population;

        public void AddPopulation(int value)
        {
            _population += value;
            if (_population > 100)
            {
                _population = 100;
            }
        }
    }
}