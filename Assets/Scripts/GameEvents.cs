using System;
using Inventory;
using Items;

// Event Bus
public static class GameEvents
{
    public static event Action<InventoryContainer> OnExternalContainerOpen;

    public static event Action OnSaveRequested; 
    
    public static void RequestOpenExternalContainer(InventoryContainer container) => 
        OnExternalContainerOpen?.Invoke(container);
    
    public static void RequestSave() => OnSaveRequested?.Invoke();

    public static event Action<ItemData> OnShowTooltipRequested;
    public static event Action OnHideTooltipRequested;

    public static void RequestShowTooltip(ItemData item) => OnShowTooltipRequested?.Invoke(item);
    public static void RequestHideTooltip() => OnHideTooltipRequested?.Invoke();

    public static event Action OnCloseMenusRequested;
    
    public static void RequestCloseMenus() => OnCloseMenusRequested?.Invoke();
}