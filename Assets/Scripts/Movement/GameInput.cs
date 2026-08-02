using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    [RequireComponent(typeof(InputSystem_Actions))]
    public class GameInput : MonoBehaviour
    {
        public static InputSystem_Actions Actions { get; private set; }
        public static PlayerInput System { get; private set; }
        
        private void Awake()
        {
            if (Actions != null) return;

            transform.parent = null;
            DontDestroyOnLoad(gameObject);

            Actions = new InputSystem_Actions();
            Actions.Enable();

            System = GetComponent<PlayerInput>();
        }

        private void OnDisable()
        {
            Actions.Player.Disable();
        }

        private void OnDestroy()
        {
            Actions.Dispose();
        }
    }
}
