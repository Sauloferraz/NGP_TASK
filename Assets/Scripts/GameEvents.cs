using System;
using Inventory;

public class GameEvents
{
    public static event Action OnTogglePlayerInventory;
    public static event Action<InventoryContainer> OnExternalContainerOpen;

    public static event Action OnSaveRequested;
    
    public static void RequestTogglePlayerInventory() => OnTogglePlayerInventory?.Invoke(); 
    
    public static void RequestOpenExternalContainer(InventoryContainer container) => 
        OnExternalContainerOpen?.Invoke(container);
    
    public static void RequestSave() => OnSaveRequested?.Invoke();
}