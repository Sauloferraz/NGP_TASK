using System;
using System.Collections.Generic;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    /// <summary>
    /// Acts as the central arbiter for complex transactions between different inventory containers.
    /// <para><b>Philosophy:</b> It implements the Mediator pattern. Since individual <see cref="InventoryContainer"/>s 
    /// should not be tightly coupled or directly manipulate each other's data, this manager safely handles 
    /// cross-container operations (like Swapping) and ensures atomic state changes.</para>
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }
        
        public List<InventoryContainer> allActiveContainers = new List<InventoryContainer>();
        
        private void Awake()
        {
            if (Instance && Instance != this) Destroy(gameObject);
            else Instance = this;
        }
        
        public void SwapItems(InventoryContainer sourceContainer, int sourceIndex, InventoryContainer targetContainer, int targetIndex)
        {
            if (sourceIndex < 0 || sourceIndex >= sourceContainer.slots.Count) return;
            if (targetIndex < 0 || targetIndex >= targetContainer.slots.Count) return;

            (sourceContainer.slots[sourceIndex], targetContainer.slots[targetIndex]) = (targetContainer.slots[targetIndex], sourceContainer.slots[sourceIndex]);

            // Above is the same as:
            // InventorySlotData temp = sourceContainer.slots[sourceIndex];
            // sourceContainer.slots[sourceIndex] = targetContainer.slots[targetIndex];
            // targetContainer.slots[targetIndex] = temp;
            
            // Updates both containers
            sourceContainer.UpdateInventoryUI();
            if (sourceContainer != targetContainer)
            {
                targetContainer.UpdateInventoryUI();
            }
            
            // Saves the changes
            GameEvents.RequestSave();
        } 
    }
}
    
