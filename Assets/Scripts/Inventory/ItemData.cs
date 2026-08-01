using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite sprite;
        public bool stackable = true;
    }
}
