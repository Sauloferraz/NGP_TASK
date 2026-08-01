using System;
using Items;

namespace Inventory
{
    /// <summary>
    /// A pure data structure representing the contents of a single inventory slot.
    /// <para><b>Philosophy:</b> Acts as the 'Model' in MVC and loosely follows the Data Transfer Object (DTO) pattern. 
    /// Being a plain, serializable C# class (non-MonoBehaviour) ensures that the game's state can be easily 
    /// serialized, saved, and loaded without any reliance on Unity's UI hierarchy or GameObject state.</para>
    /// </summary>
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
