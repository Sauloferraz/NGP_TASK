using System.Collections.Generic;
using System.IO;
using Inventory;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;

namespace Saving
{
    public class SaveSystem : MonoBehaviour
    {
        public ItemDatabase database;

        private string _savePath;

        private void Awake()
        {
            _savePath = Application.persistentDataPath + "/inventory.json";
        }

        [Button("Save Inventory", ButtonSizes.Large)]
        public void Save()
        {
            List<InventorySlotData> activeSlots = InventoryManager.Instance.slots;
            InventorySaveData saveData = new InventorySaveData();

            foreach (var slot in activeSlots)
            {
                SlotSaveData slotData = new SlotSaveData();

                if (slot.IsEmpty)
                {
                    slotData.itemID = -1;
                }
                else
                {
                    slotData.itemID = slot.itemData.id;
                }
                
                saveData.savedSlots.Add(slotData);
            }
            
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(_savePath, json);
            
            Debug.Log($"Inventory saved to {_savePath}");
        }

        [Button("Load Inventory", ButtonSizes.Large)]
        public void Load()
        {
            if (!File.Exists(_savePath))
            {
                Debug.LogWarning("No save file found!");
                return;
            }

            string json = File.ReadAllText(_savePath);
            InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);

            List<InventorySlotData> reconstructedSlots = new List<InventorySlotData>();

            foreach (var savedSlot in saveData.savedSlots)
            {
                InventorySlotData newSlot = new InventorySlotData();

                if (savedSlot.itemID != -1)
                {
                    newSlot.itemData = database.GetItemByID(savedSlot.itemID);
                }
                
                reconstructedSlots.Add(newSlot);
            }
            
            InventoryManager.Instance.OverwriteFromSave(reconstructedSlots);
            
            Debug.Log("Inventory loaded successfully");
        }
    }
}
