using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public InputActionReference moveAction;

        private Rigidbody2D rb;
        private Vector2 moveInput;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        
            rb.gravityScale = 0f;
            rb.freezeRotation = true; 
        }
        
        private void OnEnable()
        {
            if (moveAction) moveAction.action.Enable();
        }

        private void OnDisable()
        {
            if (moveAction) moveAction.action.Disable();
        }
        
        private void Update()
        {
            if (moveAction)
            {
                moveInput = moveAction.action.ReadValue<Vector2>();
            }
        }
        
        private void FixedUpdate()
        {
            rb.linearVelocity = moveInput.normalized * moveSpeed;
        }
    }
}
