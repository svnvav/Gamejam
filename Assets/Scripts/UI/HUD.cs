using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private RaceInfoList _raceInfoList;
        [SerializeField] private UberStar[] _stars;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _racePanel;

        public void Pause(bool pause)
        {
            _pausePanel.SetActive(pause);
        }

        public void ShowStartPanel()
        {
            _startPanel.SetActive(true);
        }
        
        public void ShowRacePanel(Race race)
        {
            
        }
        
        public void StepUpdate()
        {
            _raceInfoList.StepUpdate();
        }

        public void AddRace(RaceOnPlanet raceOnPlanet)
        {
            _raceInfoList.AddRace(raceOnPlanet);
        }
        
        public void RemoveRace(RaceOnPlanet raceOnPlanet, int index)
        {
            _raceInfoList.RemoveRace(raceOnPlanet);
            _stars[index].Fade();
        }
    }
}