using UnityEngine;

namespace Inventory
{
    public class TestScript : MonoBehaviour
    {
        public InventoryManager inventoryManager;

        public ItemData[] itemsToPickup;

        public void PickupItem(int id)
        {
            bool result = inventoryManager.AddItem(itemsToPickup[id]);
            if (result == true)
            {
                Debug.Log("Inventory full, failed to pickup item");
            }
        }
    }
}
