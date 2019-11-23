
using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private Planet Planet;
        [SerializeField] private int _index;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(_index);
            }
        }
    }
}