using UnityEngine;

namespace Interactables
{
    public class ProximityOutline : MonoBehaviour
    {
        [SerializeField] public GameObject outline;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                outline.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                outline.SetActive(false);
            }
        }
    }
}
