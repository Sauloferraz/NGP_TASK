using System;
using Inventory;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(InventoryContainer))]
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private int capacity = 5;
        private InventoryContainer _myInventory;

        private void Awake()
        {
            _myInventory = GetComponent<InventoryContainer>();
        }

        [Button]
        public void Interact()
        {
            GameEvents.RequestOpenExternalContainer(_myInventory);
        }
    }
}
