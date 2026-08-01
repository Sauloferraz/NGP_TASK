using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        [FormerlySerializedAs("ID")]
        public int id = -1;
        
        public string itemName;
        public Sprite sprite;
        
        public bool stackable = false; // redundant
        [EnableIf("stackable")] public int maxStacks = 1;
    }
}
