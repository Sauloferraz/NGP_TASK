using System.Collections.Generic;

namespace Saving
{
    [System.Serializable]
    public class SlotSaveData
    {
        public int itemID;
    }

    [System.Serializable]
    public class InventorySaveData
    {
        public List<SlotSaveData> savedSlots = new List<SlotSaveData>();
    }
}
