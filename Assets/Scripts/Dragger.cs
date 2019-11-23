
using System;
using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private Planet _planet;
        [SerializeField] private int _index;

        private void OnMouseDown()
        {
            GameController.Instance.OnPlanetClick(_planet, _index);
        }
    }
}