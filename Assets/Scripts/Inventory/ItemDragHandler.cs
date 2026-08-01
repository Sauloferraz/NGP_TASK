using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _originalParent;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Start moving the item
            _originalParent = transform.parent;
            transform.SetParent(transform.root);
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.6f;
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            // Item follows the mouse position
            transform.position = eventData.position;
        }
        
        // The code in here should probably be put into another function
        public void OnEndDrag(PointerEventData eventData)
        {
            // Releasing the item
            
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            
            // Checks if the pointer is hovering an InventorySlot
            InventorySlot targetSlot = eventData.pointerEnter?.GetComponentInParent<InventorySlot>();

            if (!targetSlot && eventData.pointerEnter)
                targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlot>();
            
            // Saves the slot the item originally came from
            InventorySlot originalSlot = _originalParent.GetComponentInParent<InventorySlot>();

            if (targetSlot)
            {
                if (targetSlot.currentItem)
                {
                    targetSlot.currentItem.transform.SetParent(originalSlot.transform);
                    originalSlot.currentItem = targetSlot.currentItem;
                    targetSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    originalSlot.currentItem = null;
                }
            
                transform.SetParent(targetSlot.transform);
                targetSlot.currentItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            }
            else
            {
                transform.SetParent(_originalParent);
            }
        
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
