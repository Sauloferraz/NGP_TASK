using Items;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    /// <summary>
    /// Used as the View/Presenter of the Item in the current UI slot. (e.g., displaying the Sprite).
    /// <para><b>Philosophy:</b> Acts as a pure 'View' component in the MVC architecture.
    /// It is entirely "dumb", containing zero game logic, state, or persistent data. It simply reflects the data given to it.
    /// </para>
    /// </summary>
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
