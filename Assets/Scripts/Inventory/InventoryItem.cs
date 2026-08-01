using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [RequireComponent(typeof(ItemDragHandler))]
    public class InventoryItem : MonoBehaviour
    {
        [FoldoutGroup("UI")] public Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void UpdateIcon(ItemData newItemData)
        {
            image.sprite = newItemData.sprite;
        }
    }
}
