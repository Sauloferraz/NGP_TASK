using System;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public CanvasGroup playerGroup;
        public CanvasGroup externalGroup;
        
        //TODO: Verificar uma forma melhor de fazer essa referência
        public InventoryUI externalInventory;

        private bool _isInventoryOpen = false;
        
        private void Awake()
        {
            SetPanelActive(playerGroup, false);
            SetPanelActive(externalGroup, false);
        }

        private void OnEnable()
        {
            GameEvents.OnTogglePlayerInventory += TogglePlayerInventory;
            GameEvents.OnExternalContainerOpen += OpenExternalInventory;
        }
        
        private void OnDisable()
        {
            GameEvents.OnTogglePlayerInventory -= TogglePlayerInventory;
            GameEvents.OnExternalContainerOpen -= OpenExternalInventory;
        }

        private void SetPanelActive(CanvasGroup group, bool isActive)
        {
            group.alpha = isActive ? 1 : 0;
            group.interactable = isActive ? true : false;
            group.blocksRaycasts = isActive ? true : false;
        }
        
        [Button]
        public void TogglePlayerInventory()
        {
            _isInventoryOpen = !_isInventoryOpen;
            SetPanelActive(playerGroup, _isInventoryOpen);
            
            if(!_isInventoryOpen)
                SetPanelActive(externalGroup, false);
        }

        public void OpenExternalInventory(InventoryContainer externalContainer)
        {
            _isInventoryOpen = true;
            SetPanelActive(externalGroup, true);
            
            externalInventory.Bind(externalContainer);
            
            SetPanelActive(externalGroup, true);
        }
    }
}
