using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        public string containerID;
        
        public int capacity = 5;
        
        public List<InventorySlotData> slots = new List<InventorySlotData>();
        
        public event Action OnInventoryUpdated;
        
        private void Awake()
        {
            InitializeEmptyInventory();
        }

        // This could be optimized further through script execution order and preferably no dependencies
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => InventoryManager.Instance);
            
            if (!InventoryManager.Instance.allActiveContainers.Contains(this))
            {
                InventoryManager.Instance.allActiveContainers.Add(this);
            }
        }

        private void OnDisable()
        {
            if (!InventoryManager.Instance) return;
            
            // TODO: Reformular isso aqui
            if (InventoryManager.Instance.allActiveContainers.Contains(this))
            {
                InventoryManager.Instance.allActiveContainers.Remove(this);
            }
        }

        private void InitializeEmptyInventory()
        {
            slots.Clear();
            for (int i = 0; i < capacity; i++)
            {
                slots.Add(new InventorySlotData());
            }
        }
        
        [Button]
        public bool AddItem(ItemData item)
        {
            InventorySlotData emptySlot = slots.Find(s => s.IsEmpty);
        
            if (emptySlot != null)
            {
                emptySlot.itemData = item;
            
                NotifyUpdated();
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
            
            NotifyUpdated();
            GameEvents.RequestSave();
        }
        
        // Função pública para avisar a UI de que algo mudou (usaremos no Drag and Drop)
        public void NotifyUpdated()
        {
            OnInventoryUpdated?.Invoke();
        }
    
        // Usado pelo sistema de save
        public void OverwriteFromSave(List<InventorySlotData> loadedSlots)
        {
            slots = loadedSlots;
            NotifyUpdated();
        }
    }
}
