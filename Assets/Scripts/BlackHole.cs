using UnityEngine;

namespace Plarium.Gamejam2019
{
    public class BlackHole : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("HeavenBody"))
            {
                var planet = other.GetComponent<Planet>();
                if (planet != null)
                {
                    planet.Die();
                }
            }
        }
    }
}