using UnityEngine;
using UnityEngine.UI;

namespace Plarium.Gamejam2019
{
    public class RaceInfo : MonoBehaviour
    {
        public RaceOnPlanet RaceOnPlanet;

        [SerializeField] private Image _image;
        [SerializeField] private Text _percentageText;

        public void StepUpdate()
        {
            _percentageText.text = $"{RaceOnPlanet.Population}%";
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}