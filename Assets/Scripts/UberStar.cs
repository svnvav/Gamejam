using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class UberStar : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Fade()
        {
            _animator.Play("Fade");
        }
    }
}