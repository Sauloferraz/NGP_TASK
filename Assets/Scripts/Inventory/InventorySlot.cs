using UnityEngine;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public int slotIndex { get; private set; }

        public InventoryItem visualItem;

        public void UpdateVisuals(InventorySlotData data, int index)
        {
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
