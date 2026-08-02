using System;
using Inventory;
using UnityEngine;

namespace Interactables
{
    public class Cauldron : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; set; }

        private InventoryContainer _cauldronContainer;

        private void Awake()
        {
            CanInteract = true;
            _cauldronContainer = GetComponent<InventoryContainer>();
        }

        public void Interact()
        {
            if (!CanInteract)
            {
                CloseCauldron();
                GameEvents.RequestCloseMenus();
            }
            else
            {
                OpenCauldron();
            }
        }

        private void OpenCauldron()
        {
            CanInteract = false;
            GameEvents.RequestOpenExternalContainer(_cauldronContainer);
        }

        private void CloseCauldron()
        {
            CanInteract = true;
        }
    }
}
