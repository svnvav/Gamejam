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
    }
}