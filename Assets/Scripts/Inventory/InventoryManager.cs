using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject inventoryItemPrefab;
        
        public InventorySlot[] inventorySlots;

        [Button]
        public void AddItem(ItemData item)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                
                Debug.Log($"Slot: {slot}");
                Debug.Log($"Item: {item}");
                Debug.Log($"Sprite: {item.sprite}");
                
                if (!itemInSlot)
                {
                    SpawnItem(item, slot);
                    return;
                }
            }
        }
        
        public void SpawnItem(ItemData itemData, InventorySlot inventorySlot)
        {
            GameObject newItemGO = Instantiate(inventoryItemPrefab, inventorySlot.transform);
            InventoryItem newItem = newItemGO.GetComponent<InventoryItem>();
            newItem.Init(itemData);
        }
    }
}
