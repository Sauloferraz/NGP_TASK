using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Tooltip("Arraste os seus GameObjects de InventorySlot aqui pelo Inspector")]
        public InventorySlot[] uiSlots;

        public InventoryContainer autoBindContainer;
        
        private InventoryContainer currentContainer;

        private void Start()
        {
            if (autoBindContainer)
            {
                Bind(autoBindContainer);
            }
        }

        public void Bind(InventoryContainer container)
        {
            if (currentContainer)
            {
                currentContainer.OnInventoryUpdated -= RefreshUI;
            }
            
            currentContainer = container;

            if (currentContainer)
            {
                currentContainer.OnInventoryUpdated += RefreshUI;
                RefreshUI();
            }
        }
        
        private void RefreshUI()
        {
            if (!currentContainer) return;

            List<InventorySlotData> dataSlots = currentContainer.slots;

            for (int i = 0; i < uiSlots.Length; i++)
            {
                if (i < dataSlots.Count)
                {
                    uiSlots[i].gameObject.SetActive(true);
                    uiSlots[i].UpdateVisuals(currentContainer, dataSlots[i], i);
                }
                else
                {
                    uiSlots[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
