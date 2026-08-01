using System;
using System.Collections.Generic;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
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

            // Acima é o mesmo que isso:
            // InventorySlotData temp = sourceContainer.slots[sourceIndex];
            // sourceContainer.slots[sourceIndex] = targetContainer.slots[targetIndex];
            // targetContainer.slots[targetIndex] = temp;
            
            // Avisa os dois containers que eles mudaram. As UIs deles vão se atualizar sozinhas!
            sourceContainer.NotifyUpdated();
            if (sourceContainer != targetContainer)
            {
                targetContainer.NotifyUpdated();
            }
        } 
    }
}
    
