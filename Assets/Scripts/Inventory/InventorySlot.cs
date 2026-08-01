using UnityEngine;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public bool IsEmpty => !currentItem;
        
        // Saves the currentItem in this slot
        public InventoryItem currentItem;
    }
}
