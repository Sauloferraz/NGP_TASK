using System;
using Items;

namespace Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public ItemData itemData;

        public bool IsEmpty => !itemData;

        public InventorySlotData()
        {
            Clear();
        }
        
        public InventorySlotData(ItemData itemData, int count)
        {
            this.itemData = itemData;
        }

        public void Clear()
        {
            itemData = null;
        }
    }
}
