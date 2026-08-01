using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class TestScript : MonoBehaviour
    {
        private InputSystem_Actions actions;

        private void Awake()
        {
            actions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            actions.Player.Enable();
        }

        private void Update()
        {
            if (actions.Player.Interact.WasPressedThisFrame())
            {
                GameEvents.RequestTogglePlayerInventory();
            }
        }

        private void OnDisable()
        {
            actions.Player.Disable();
        }

        private void OnDestroy()
        {
            actions.Dispose();
        }

    }
}
