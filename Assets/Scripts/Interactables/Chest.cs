using System;
using Inventory;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(InventoryContainer))]
    public class Chest : MonoBehaviour, IInteractable
    {
        public bool CanInteract { get; set; }
        
        [SerializeField] private int capacity = 5;
        private InventoryContainer _myInventory;
        
        public bool isOpen;

        private void Awake()
        {
            CanInteract = true;
            _myInventory = GetComponent<InventoryContainer>();
        }
        
        private void OnEnable()
        {
            GameEvents.OnCloseMenusRequested += CloseChest;
        }

        private void OnDisable()
        {
            GameEvents.OnCloseMenusRequested -= CloseChest;
        }
        
        [Button]
        public void Interact()
        {
            if (isOpen)
            {
                CloseChest();
                GameEvents.RequestCloseMenus();
            }
            else
            {
                OpenChest();
            }
        }

        private void OpenChest()
        {
            isOpen = true;
            CanInteract = false;
            GameEvents.RequestOpenExternalContainer(_myInventory);
        }

        private void CloseChest()
        {
            isOpen = false;
            CanInteract = true;
        }
    }
}
