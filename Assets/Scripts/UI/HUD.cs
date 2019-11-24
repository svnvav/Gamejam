using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private RaceInfoList _raceInfoList;
        [SerializeField] private UberStar[] _stars;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private RacePanel _racePanel;
        [SerializeField] private GameObject _loseScreen;

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
            _racePanel.SetText(race.Description);
            _racePanel.SetFaceImage(race.HelloSprite);
            _racePanel.Show();
        }
        
        public void StepUpdate()
        {
            _raceInfoList.StepUpdate();
        }

        public void AddRace(RaceOnPlanet raceOnPlanet)
        {
            _raceInfoList.AddRace(raceOnPlanet);
            ShowRacePanel(raceOnPlanet.Race);
        }
        
        public void RemoveRace(RaceOnPlanet raceOnPlanet, int index)
        {
            _raceInfoList.RemoveRace(raceOnPlanet);
            _stars[index].Fade();
        }

        public void ShowLoseScreen()
        {
            _loseScreen.SetActive(true);
        }
    }
}