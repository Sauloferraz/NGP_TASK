using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Connects a specific <see cref="InventoryContainer"/> to an array of <see cref="InventorySlot"/> UI elements.
    /// <para><b>Philosophy:</b> Implements the Observer pattern. It acts as a "Television" that tunes into a specific 
    /// data "Channel" (the Container). It listens for state changes and redraws the UI dynamically, ensuring 
    /// the UI never dictates the game state.</para>
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        public InventorySlot[] uiSlots;

        public InventoryContainer autoBindContainer;
        
        private InventoryContainer _currentContainer;

        private void Start()
        {
            if (autoBindContainer)
            {
                Bind(autoBindContainer);
            }
        }

        public void Bind(InventoryContainer container)
        {
            if (_currentContainer)
            {
                _currentContainer.OnInventoryUpdated -= RefreshUI;
            }
            
            _currentContainer = container;

            if (!_currentContainer) return;
            _currentContainer.OnInventoryUpdated += RefreshUI;
            RefreshUI();
        }
        
        private void RefreshUI()
        {
            if (!_currentContainer) return;

            List<InventorySlotData> dataSlots = _currentContainer.slots;

            for (int i = 0; i < uiSlots.Length; i++)
            {
                if (i < dataSlots.Count)
                {
                    uiSlots[i].gameObject.SetActive(true);
                    uiSlots[i].UpdateVisuals(_currentContainer, dataSlots[i], i);
                }
                else
                {
                    //uiSlots[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
