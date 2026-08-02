using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    /// <summary>
    /// Catches mouse drag events and handles the visual illusion of moving items across the Canvas.
    /// <para><b>Philosophy:</b> Acts as a 'Controller' in MVC. It translates user input into actionable commands 
    /// and delegates the actual data mutation to the <see cref="InventoryManager"/>. This strictly separates 
    /// UI gesture logic from the core business logic of the inventory.</para>
    /// </summary>
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _originalParent;
        private Vector3 _originalPosition;
        private CanvasGroup _canvasGroup;
        private InventorySlot _sourceSlot;
        
        private void OnEnable() => GameEvents.OnCloseMenusRequested += ResetDrag;
        private void OnDisable() => GameEvents.OnCloseMenusRequested -= ResetDrag;
        
        private void Awake()
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
            ResetDrag();
            
           if (!eventData.pointerEnter) return;
           
           InventorySlot targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlot>();
           
           if ((targetSlot && _sourceSlot))
           {
               if (targetSlot == _sourceSlot) return;
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

        private void ResetDrag()
        {
            if (!_originalParent) return;
            
            transform.SetParent(_originalParent);
            transform.position = _originalPosition;
            
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
        }
    }
}
