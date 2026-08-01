using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject inventoryItemPrefab;
        public InventorySlot[] inventorySlots;
        
        [Button]
        public bool AddItem(ItemData item)
        {
            // Checks if any slot has the same item with count lower than max and if it's stackable.
            
            foreach (InventorySlot slot in inventorySlots)
            {
                InventoryItem itemInSlot = slot.currentItem;
                
                if (itemInSlot && 
                    itemInSlot.Data == item 
                    && itemInSlot.Data.stackable
                    && itemInSlot.ItemCount < itemInSlot.Data.maxStacks)
                {
                    itemInSlot.Increment();
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
            
            foreach (InventorySlot slot in inventorySlots)
            {
                InventoryItem itemInSlot = slot.currentItem;
                
                if (!itemInSlot)
                {
                    SetItem(slot, item);
                    return true;
                }
            }

            return false;
        }

        private void SetItem(InventorySlot inventorySlot, ItemData itemData)
        {
            GameObject newItemObj = Instantiate(inventoryItemPrefab, inventorySlot.transform);
            InventoryItem newItem = newItemObj.GetComponent<InventoryItem>();
            
            newItem.Init(itemData);
            
            inventorySlot.currentItem = newItem;
        }
    }
}
