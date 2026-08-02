using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Represents the local data state of an inventory for a specific entity (Player, Chest, Shelf, Bag...).
    /// <para><b>Philosophy:</b> Follows the Rich Domain Model principle. It acts as the Single Source of Truth for its own data, managing additions and removals locally.
    /// It is completely decoupled from the UI, using an Event-Driven approach (Observer pattern) to notify listeners when its state changes.</para>
    ///
    /// The capacity field is defined in the Inspector, and ideally, the actual slot objects should be generated dynamically based on that, instead of being dragged from the Hierarchy.
    /// But since time is short and this is a prototype, I decided to keep things simple and fast.
    /// The class has an event Action, called when the container is updated by Player Actions, working as a bridge to the View and Data saves.
    /// </summary>
    public class InventoryContainer : MonoBehaviour
    {
        public string containerID;
        
        public int capacity = 5;
        
        public List<InventorySlotData> slots = new List<InventorySlotData>();
        
        public event Action OnInventoryUpdated;

        // This could be optimized further through script execution order, and/or preferably no dependencies to InventoryManager
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => InventoryManager.Instance);
            
            if (!InventoryManager.Instance.allActiveContainers.Contains(this))
            {
                InventoryManager.Instance.allActiveContainers.Add(this);
            }
            
            UpdateInventoryUI();
        }

        // This could also be optimized further to not have any dependencies.
        private void OnDisable()
        {
            if (!InventoryManager.Instance) return;
            
            // TODO: Reformular isso aqui
            if (InventoryManager.Instance.allActiveContainers.Contains(this))
            {
                InventoryManager.Instance.allActiveContainers.Remove(this);
            }
        }
        
        [Button] // Adding an Item to the Container saves it to the corresponding Slot, which is necessary to the Slot-Persistant Save System.
        public bool AddItem(ItemData item)
        {
            InventorySlotData emptySlot = slots.Find(s => s.IsEmpty);
        
            if (emptySlot != null)
            {
                emptySlot.itemData = item;
            
                UpdateInventoryUI();
                GameEvents.RequestSave();
                return true;
            }

            return false;
        }

        [Button]
        public void RemoveItemAt(int index)
        {
            if(index < 0 || index >= slots.Count) return;
            
            slots[index].Clear();
            
            UpdateInventoryUI();
            GameEvents.RequestSave();
        }
        
        public void UpdateInventoryUI()
        {
            OnInventoryUpdated?.Invoke();
        }
    
        // Used for SaveSystem AutoSave
        public void OverwriteFromSave(List<InventorySlotData> loadedSlots)
        {
            slots = loadedSlots;
            UpdateInventoryUI();
        }
    }
}
