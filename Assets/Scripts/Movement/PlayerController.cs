using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        [FoldoutGroup("Movement Settings")]
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveInput;
        private Animator _animator;
        
        [SerializeField] private float baseSpeed = 10f;

        public Vector2 LastMoveDirection { get; private set; } = new Vector2(0, -1);
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.freezeRotation = true;

            LastMoveDirection = new Vector2(0, -1);
        }

        private void Update()
        {
            MovementDirection();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovementDirection()
        {
            _moveInput = GameInput.Actions.Player.Move.ReadValue<Vector2>();

            if (_moveInput.magnitude > 0.01f)
            {
                LastMoveDirection = _moveInput.normalized;
                
                GameEvents.RequestCloseMenus();
            }

            UpdateAnimations();
        }

        private void MovePlayer()
        {
            _rigidbody2D.linearVelocity = _moveInput.normalized * baseSpeed;
        }

        private void UpdateAnimations()
        {
            if (!_animator) return;
            
            _animator.SetFloat(Horizontal, LastMoveDirection.x);
            _animator.SetFloat(Vertical, LastMoveDirection.y);
            
            _animator.SetFloat(Speed, _moveInput.magnitude);
        }
    }
}
