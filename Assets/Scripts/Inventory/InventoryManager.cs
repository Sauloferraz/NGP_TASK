using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private int capacity = 20;
        public List<InventorySlotData> slots = new List<InventorySlotData>();

        public event Action OnInventoryUpdated;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            InitInventory();
        }

        private void InitInventory()
        {
            slots.Clear();
            
            for (int i = 0; i < capacity; i++)
            {
                slots.Add(new InventorySlotData(null, 0));
            }
        }

        [Button]
        public bool AddItem(ItemData item, int amount = 1)
        {
            // Busca o primeiro slot vazio na lista
            InventorySlotData emptySlot = slots.Find(s => s.IsEmpty);
        
            if (emptySlot != null)
            {
                emptySlot.itemData = item;
                emptySlot.count = 1; // Adiciona apenas 1, sem stacking
            
                OnInventoryUpdated?.Invoke(); // Avisa a UI
                return true;
            }

            // Se chegou aqui, não achou slot vazio (Inventário cheio)
            return false;
        }

        public void SwapSlots(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= slots.Count || indexB < 0 || indexB >= slots.Count) return;

            (slots[indexA], slots[indexB]) = (slots[indexB], slots[indexA]);
            
            // Above is the same as:
            // InventorySlotData temp = slots[indexA];
            // slots[indexA] = slots[indexB];
            // slots[indexB] = temp;

            OnInventoryUpdated?.Invoke(); // Avisa a UI
        }
    }
}
