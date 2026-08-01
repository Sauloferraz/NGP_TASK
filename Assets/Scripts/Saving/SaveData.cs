using System.Collections.Generic;

namespace Saving
{
    [System.Serializable]
    public class SlotSaveData
    {
        public int itemID;
    }

    [System.Serializable]
    public class ContainerSaveData
    {
        public string containerID;
        public List<SlotSaveData> savedSlots = new List<SlotSaveData>();
    }

    [System.Serializable]
    public class GlobalSaveData
    {
        public List<ContainerSaveData> containers = new List<ContainerSaveData>();
    }
}
