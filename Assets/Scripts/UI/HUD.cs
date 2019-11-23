using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private RaceInfoList _raceInfoList;

        public void StepUpdate()
        {
            _raceInfoList.StepUpdate();
        }

        public void AddRace(RaceOnPlanet raceOnPlanet)
        {
            _raceInfoList.AddRace(raceOnPlanet);
        }
    }
}