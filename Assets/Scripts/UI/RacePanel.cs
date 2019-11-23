using UnityEngine;
using UnityEngine.UI;

namespace Plarium.Gamejam2019
{
    public class RacePanel : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;
        [SerializeField] private Animator _animator;

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetFaceImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        
        public void Show()
        {
            _animator.Play("Show");
        }

        public void Hide()
        {
            _animator.Play("Hide");
        }

    }
}