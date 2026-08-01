using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _originalParent;
        private Vector3 _originalPosition;
        private CanvasGroup _canvasGroup;
        private InventorySlot _sourceSlot;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if(!_canvasGroup) _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _sourceSlot = GetComponentInParent<InventorySlot>();
            
            _originalParent = transform.parent;
            _originalPosition = transform.position;
            
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.6f;
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            // Item follows the mouse position
            transform.position = eventData.position;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            // Releasing the item
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            
           transform.SetParent(_originalParent);
           transform.position = _originalPosition;

           if (!eventData.pointerEnter) return;
           
           InventorySlot targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlot>();
           
           if (targetSlot && _sourceSlot)
           {
               InventoryManager.Instance.SwapItems(_sourceSlot.ParentContainer, _sourceSlot.SlotIndex,
                   targetSlot.ParentContainer, targetSlot.SlotIndex);
               return;
           }
               
           TrashZone trash = eventData.pointerEnter.GetComponentInParent<TrashZone>();
           
           if (trash && _sourceSlot)
           {
               _sourceSlot.ParentContainer.RemoveItemAt(_sourceSlot.SlotIndex);
           }
        }
    }
}
