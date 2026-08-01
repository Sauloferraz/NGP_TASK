using UnityEngine;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public InventoryContainer ParentContainer { get; private set; }
        public int slotIndex { get; private set; }

        public InventoryItem visualItem;

        public void UpdateVisuals(InventoryContainer container, InventorySlotData data, int index)
        {
            ParentContainer = container;
            slotIndex = index;
            
            if (data.IsEmpty)
            {
                visualItem.gameObject.SetActive(false);
            }
            else
            {
                visualItem.gameObject.SetActive(true);
                visualItem.UpdateIcon(data.itemData);
            }
        }
    }
}
