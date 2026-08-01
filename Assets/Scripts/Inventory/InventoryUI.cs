using UnityEngine;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Tooltip("Arraste os seus GameObjects de InventorySlot aqui pelo Inspector")]
        public InventorySlot[] uiSlots;

        private void Start()
        {
            // 1. A UI liga a TV e se inscreve no "megafone" do Manager
            InventoryManager.Instance.OnInventoryUpdated += RefreshUI;
        
            // 2. Atualiza a tela uma vez no início para sincronizar
            RefreshUI();
        }
        
        private void OnDestroy()
        {
            // Boa prática: se o painel do inventário for destruído, cancelamos a inscrição
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.OnInventoryUpdated -= RefreshUI;
            }
        }

        private void RefreshUI()
        {
            // Pega a nossa "planilha" atualizada
            var dataSlots = InventoryManager.Instance.slots;

            // Passa por todos os slots da tela
            for (int i = 0; i < uiSlots.Length; i++)
            {
                // Garantia de segurança caso você tenha mais slots na UI do que na 'capacity'
                if (i < dataSlots.Count) 
                {
                    // No Passo 4 nós vamos criar essa função no seu InventorySlot!
                    uiSlots[i].UpdateVisuals(dataSlots[i], i);
                }
            }
        }
    }
}
