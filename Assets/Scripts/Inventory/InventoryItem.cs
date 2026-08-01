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
        [FoldoutGroup("UI")] public TextMeshProUGUI countText;
        
        public ItemData Data { get; private set; }
        public int ItemCount { get; private set; } = 1;

        private void Awake()
        {
            image = GetComponent<Image>();
            countText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(ItemData newItemData)
        {
            Data = newItemData;
            image.sprite = newItemData.sprite;
            
            RefreshCount();
        }

        public void RefreshCount()
        {
            countText.text = ItemCount.ToString();
            countText.gameObject.SetActive(ItemCount > 1);
        }

        public void Increment()
        {
            ItemCount++;
            RefreshCount();
        }

        public void Decrement()
        {
            ItemCount--;
            RefreshCount();
        }

        public void SetCount(int count)
        {
            ItemCount = count;
            RefreshCount();
        }
    }
}
