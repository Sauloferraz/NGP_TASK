using System;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public CanvasGroup externalGroup;
        
        public InventoryUI externalUI;
        
        private void Awake()
        {
            SetPanelActive(externalGroup, false);
        }

        private void OnEnable()
        {
            GameEvents.OnExternalContainerOpen += OpenExternalInventory;
            
            GameEvents.OnCloseMenusRequested += ForceCloseAll;
        }
        
        private void OnDisable()
        {
            GameEvents.OnExternalContainerOpen -= OpenExternalInventory;
            
            GameEvents.OnCloseMenusRequested -= ForceCloseAll;
        }

        private void SetPanelActive(CanvasGroup group, bool isActive)
        {
            group.alpha = isActive ? 1 : 0;
            group.interactable = isActive;
            group.blocksRaycasts = isActive;
        }

        private void OpenExternalInventory(InventoryContainer externalContainer)
        {
            externalUI.Bind(externalContainer);
            externalContainer.UpdateInventoryUI();
            
            SetPanelActive(externalGroup, true);
        }

        private void ForceCloseAll()
        {
            SetPanelActive(externalGroup, false);
        }
    }
}
