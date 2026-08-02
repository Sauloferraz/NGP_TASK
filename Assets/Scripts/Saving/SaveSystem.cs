using System;
using System.Collections.Generic;
using System.IO;
using Inventory;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;
using File = System.IO.File;

namespace Saving
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        
        public ItemDatabase database;
        
        private string _savePath;

        private void Awake()
        {
            if (Instance && Instance != this) Destroy(gameObject);
            else Instance = this;
            
            _savePath = Application.persistentDataPath + "/global_inventory.json";
        }
        
        private void OnEnable()
        {
            GameEvents.OnSaveRequested += Save;
        }

        private void OnDisable()
        {
            GameEvents.OnSaveRequested -= Save;
        }
        
        public bool HasSaveFile()
        {
            return File.Exists(_savePath);
        }

        [Button("Delete Save", ButtonSizes.Large)]
        public void DeleteSaveFile()
        {
            if (!File.Exists(_savePath)) return;
            File.Delete(_savePath);
            Debug.Log("<color=magenta>[NEW GAME]</color> Save file deleted from disk.");
        }
        
        [Button("Save", ButtonSizes.Large)]
        public void Save()
        {
            // Clears any 'null' or destroyed containers (fixes Unity Editor static list bug)
            InventoryManager.Instance.allActiveContainers.RemoveAll(c => !c);
        
            Debug.Log($"<color=cyan>[SAVE]</color> Started saving. Found {InventoryManager.Instance.allActiveContainers.Count} active containers in the scene.");

            GlobalSaveData globalSave = new GlobalSaveData();

            foreach (InventoryContainer container in InventoryManager.Instance.allActiveContainers)
            {
                if (string.IsNullOrEmpty(container.containerID))
                {
                    Debug.LogWarning($"<color=yellow>[SAVE WARNING]</color> Ignored a container on '{container.gameObject.name}', its ContainerID is empty!");
                    continue;
                }

                ContainerSaveData containerSave = new ContainerSaveData
                {
                    containerID = container.containerID
                };

                int itemsSavedCount = 0;
                
                foreach (InventorySlotData slot in container.slots)
                {
                    SlotSaveData slotData = new SlotSaveData();
                    if (slot.IsEmpty)
                    {
                        slotData.itemID = -1;
                    }
                    else
                    {
                        slotData.itemID = slot.itemData.id;
                        itemsSavedCount++;
                    }
                    containerSave.savedSlots.Add(slotData);
                }

                globalSave.containers.Add(containerSave);
                Debug.Log($"<color=cyan>[SAVE]</color> Saved container '{container.containerID}' with {itemsSavedCount} items.");
            }

            string json = JsonUtility.ToJson(globalSave, true);
            File.WriteAllText(_savePath, json);
            Debug.Log($"<color=green>[SAVE SUCCESS]</color> File written to: {_savePath}");
        }

        [Button("Load", ButtonSizes.Large)]
        public void Load()
        {
            // Clear any null containers
            InventoryManager.Instance.allActiveContainers.RemoveAll(c => !c);

            if (!File.Exists(_savePath))
            {
                Debug.LogError("<color=red>[LOAD ERROR]</color> No save file found at " + _savePath);
                return;
            }

            string json = File.ReadAllText(_savePath);
            GlobalSaveData globalSave = JsonUtility.FromJson<GlobalSaveData>(json);

            if (globalSave?.containers == null)
            {
                Debug.LogError("<color=red>[LOAD ERROR]</color> Failed to parse JSON. The file might be corrupted.");
                return;
            }

            Debug.Log($"<color=orange>[LOAD]</color> JSON read successfully. Found {globalSave.containers.Count} containers in the save file.");

            foreach (InventoryContainer activeContainer in InventoryManager.Instance.allActiveContainers)
            {
                ContainerSaveData matchingData = globalSave.containers.Find(x => x.containerID == activeContainer.containerID);

                if (matchingData != null)
                {
                    var reconstructedSlots = new List<InventorySlotData>();
                    int itemsLoadedCount = 0;

                    foreach (SlotSaveData savedSlot in matchingData.savedSlots)
                    {
                        InventorySlotData newSlot = new InventorySlotData();
                        if (savedSlot.itemID != -1)
                        {
                            ItemData foundItem = database.GetItemByID(savedSlot.itemID);
                            if (!foundItem) 
                            {
                                Debug.LogWarning($"<color=yellow>[LOAD WARNING]</color> Container '{activeContainer.containerID}' tried to load Item ID {savedSlot.itemID}, but it's missing in the Database!");
                            }
                            else
                            {
                                newSlot.itemData = foundItem;
                                itemsLoadedCount++;
                            }
                        }
                        reconstructedSlots.Add(newSlot);
                    }

                    activeContainer.OverwriteFromSave(reconstructedSlots);
                    Debug.Log($"<color=orange>[LOAD]</color> Successfully applied {itemsLoadedCount} items to '{activeContainer.containerID}'.");
                }
                else
                {
                    Debug.LogWarning($"<color=yellow>[LOAD WARNING]</color> Container '{activeContainer.containerID}' is in the scene but has NO DATA in the save file.");
                }
            }
        
            Debug.Log("<color=green>[LOAD SUCCESS]</color> Process finished.");
        }
    }
}
