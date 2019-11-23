using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class StartStory : MonoBehaviour
    {
        [SerializeField] private StartStorySlide[] _slides;

        private int _current;

        public void NextSlide()
        {
            _slides[_current].Hide();
            _current++;
            if (_current >= _slides.Length)
            {
                GameController.Instance.Begin();
            }
            else
            {
                _slides[_current].gameObject.SetActive(true);
                _slides[_current].Show();
            }
        }
    }
}