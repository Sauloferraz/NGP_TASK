using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// A central registry that holds references to all <see cref="ItemData"/> instances in the project.
    /// <para><b>Philosophy:</b> It acts as a bridge for the Save System, 
    /// allowing lightweight integer IDs to be serialized into JSON, which are then mapped back to their 
    /// heavy ScriptableObject references during the loading phase.</para>
    /// </summary>
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemData> allItems = new List<ItemData>();
        
        [Button]
        public void AutoAssignIDs()
        {
            // Limpa a lista de nulos caso algum item tenha sido deletado
            allItems.RemoveAll(item => item == null);

            // O índice da lista vira o ID do item! Auto-incremento perfeito e sem conflitos.
            for (int i = 0; i < allItems.Count; i++)
            {
                allItems[i].id = i;
            
                // Marca o arquivo como alterado para a Unity salvar as mudanças no disco
            #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(allItems[i]);
            #endif
            }
        
        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.SaveAssets();
            Debug.Log("IDs de todos os itens gerados com sucesso!");
        #endif
        }
        
        // Função que o SaveSystem vai usar para achar o item na hora de carregar o jogo
        public ItemData GetItemByID(int id)
        {
            // Se o ID for inválido ou não existir na lista, retorna null
            if (id < 0 || id >= allItems.Count) return null;
        
            return allItems[id];
        }
    }
}
