using System;
using Movement;
using UnityEngine;

namespace Interactables
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float interactionDistance = 1f;
        public float interactionRadius = 0.5f;
        public LayerMask interactionLayerMask;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (!GameInput.Actions.Player.Interact.WasPressedThisFrame()) return;
            OnInteract();
        }

        private void OnInteract()
        {
            Vector2 checkPosition = (Vector2)transform.position + 
                                    (_playerController.LastMoveDirection * interactionDistance);

            Collider2D hit = Physics2D.OverlapCircle(checkPosition, interactionRadius, interactionLayerMask);

            if (!hit) return;
            
            IInteractable interactable = hit.gameObject.GetComponentInParent<IInteractable>();

            interactable?.Interact();
        }
        
        private void OnDrawGizmosSelected()
        {
            if (!_playerController) _playerController = GetComponent<PlayerController>();
        
            Vector2 direction = _playerController ? _playerController.LastMoveDirection : Vector2.down;
            Vector2 checkPosition = (Vector2)transform.position + (direction * interactionDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(checkPosition, interactionRadius);
        }
    }
}
