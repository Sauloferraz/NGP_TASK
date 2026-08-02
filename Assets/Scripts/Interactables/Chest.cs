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
        private InventoryContainer _chestContainer;
        
        private void OnEnable() => GameEvents.OnCloseMenusRequested += CloseChest;
        
        private void OnDisable() => GameEvents.OnCloseMenusRequested -= CloseChest;
        
        private void Awake()
        {
            CanInteract = true;
            _chestContainer = GetComponent<InventoryContainer>();
        }
        
        [Button]
        public void Interact()
        {
            if (!CanInteract)
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
            CanInteract = false;
            GameEvents.RequestOpenExternalContainer(_chestContainer);
        }

        private void CloseChest()
        {
            CanInteract = true;
        }
    }
}
