using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public InventorySlot[] inventorySlots;

        public InventoryItem InventoryItemPrefab;
        
        private void Start()
        {
            
        }

        public void AddItem(ItemData item)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                
                if (!itemInSlot)
                {
                    SpawnItem(item, slot);
                    return;
                }
            }
        }

        public void SpawnItem(ItemData itemData, InventorySlot inventorySlot)
        {
            GameObject newItemGO = Instantiate(InventoryItemPrefab.gameObject, inventorySlot.transform);
            InventoryItem newItem = newItemGO.GetComponent<InventoryItem>();
            newItem.Init(itemData);
        }
    }
}
