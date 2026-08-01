using System;

namespace Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public ItemData itemData;
        public int count; // Will be use to stack items if time permits :)

        public bool IsEmpty => itemData == null || count <= 0;

        public InventorySlotData()
        {
            Clear();
        }
        
        public InventorySlotData(ItemData itemData, int count)
        {
            this.itemData = itemData;
            this.count = count;
        }

        public void Clear()
        {
            itemData = null;
            count = 0;
        }
    }
}
