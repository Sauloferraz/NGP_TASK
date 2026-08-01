using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(ItemDragHandler))]
    public class InventoryItem : MonoBehaviour
    {
        [HideInInspector] public ItemData Data { get; }
        
        public Image image { get; }
        
        public void Init(ItemData newItemData)
        {
            image.sprite = newItemData.image;
        }
    }
}
