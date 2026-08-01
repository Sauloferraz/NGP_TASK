using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    /// <summary>
    /// Defines the immutable, base properties of an item (Name, Sprite, Description, Max Stacks).
    /// <para><b>Philosophy:</b> Follows Data-Driven Design. By using ScriptableObjects, 
    /// memory footprint is minimized (data is shared across all instances), and Game Designers can easily 
    /// create and balance items without touching the codebase.</para>
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        [FormerlySerializedAs("ID")]
        public int id = -1;
        
        public string itemName;
        public Sprite sprite;
        [TextArea(3,5)] public string description;
        
        public bool stackable = false; // redundant
        [EnableIf("stackable")] public int maxStacks = 1;
    }
}
