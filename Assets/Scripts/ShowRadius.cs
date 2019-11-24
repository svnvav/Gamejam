using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class ShowRadius : MonoBehaviour
    {
        [SerializeField] private float _raduis;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _raduis);
        }
    }
}