using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public InventoryContainer ParentContainer { get; private set; }
        public int SlotIndex { get; private set; }

        public InventoryItem visualItem;
        
        private InventorySlotData _currentData;

        public void UpdateVisuals(InventoryContainer container, InventorySlotData data, int index)
        {
            ParentContainer = container;
            SlotIndex = index;
            _currentData = data;
            
            if (_currentData.IsEmpty)
            {
                visualItem.gameObject.SetActive(false);
            }
            else
            {
                visualItem.gameObject.SetActive(true);
                visualItem.UpdateIcon(_currentData.itemData);
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_currentData is { IsEmpty: false })
            {
                GameEvents.RequestShowTooltip(_currentData.itemData);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            GameEvents.RequestHideTooltip();
        }
    }
}
