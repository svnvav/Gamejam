using UnityEngine;

namespace Plarium.Gamejam2019
{
    [System.Serializable]
    public class RaceInfluence
    {
        [SerializeField] private Race _race;
        [SerializeField] private int _influenceOnPopulation;

        public Race Race => _race;

        public int InfluenceOnPopulation => _influenceOnPopulation;
    }
}