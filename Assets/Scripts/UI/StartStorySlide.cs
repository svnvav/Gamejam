using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class StartStorySlide : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

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