using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform originalParent;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Start moving the item
            originalParent = transform.parent;
            transform.SetParent(transform.root);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            // Item follows the mouse position
            transform.position = eventData.position;
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            // Releasing the item
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            InventorySlot dropSlot = eventData.pointerEnter?.GetComponent<InventorySlot>();

            if (!dropSlot)
            {
                GameObject dropItem = eventData.pointerEnter;
                if (dropItem)
                {
                    dropSlot = dropItem.GetComponentInParent<InventorySlot>();
                }
            }
        
            InventorySlot originalSlot = originalParent.GetComponent<InventorySlot>();

            if (dropSlot)
            {
                if (dropSlot.currentItem)
                {
                    dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                    originalSlot.currentItem = dropSlot.currentItem;
                    dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    originalSlot.currentItem = null;
                }
            
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            }
            else
            {
                transform.SetParent(originalParent);
            }
        
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
