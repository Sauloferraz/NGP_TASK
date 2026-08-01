using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(ItemDragHandler))]
    public class InventoryItem : MonoBehaviour
    {
        public ItemData data;
        public Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Init(ItemData newItemData)
        {
            data = newItemData;
            image.sprite = newItemData.sprite;
        }
    }
}
