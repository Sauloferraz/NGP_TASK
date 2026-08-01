using System;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TooltipController : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            HideTooltip();
        }

        private void OnEnable()
        {
            GameEvents.OnShowTooltipRequested += ShowTooltip;
            GameEvents.OnHideTooltipRequested += HideTooltip;
        }
        
        private void Update()
        {
            // Only calculate mouse position if the tooltip is currently visible
            if (_canvasGroup.alpha > 0)
            {
                FollowMouse();
            }
        }
        
        private void OnDisable()
        {
            GameEvents.OnShowTooltipRequested -= ShowTooltip;
            GameEvents.OnHideTooltipRequested -= HideTooltip;
        }
        
        private void ShowTooltip(ItemData itemData)
        {
            nameText.text = itemData.itemName;
            //descriptionText.text = itemData.description;

            // Force position update instantly before showing to prevent a 1-frame visual jump
            FollowMouse(); 
        
            _canvasGroup.alpha = 1f;
        }
        
        
        private void HideTooltip()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        private void FollowMouse()
        {
            // Read mouse position using the New Input System
            if (Mouse.current == null) return;
            
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            transform.position = mousePosition;
        }
    }
}
