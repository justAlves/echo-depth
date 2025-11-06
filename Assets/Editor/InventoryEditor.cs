#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{

    [SerializeField, ItemSelector]
    private Items selectedItem;

    public override void OnInspectorGUI()
    {
        Inventory inv = (Inventory)target;

        DrawDefaultInspector(); // mostra lista debugItems

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Adicionar Item Manualmente", EditorStyles.boldLabel);

        // Dropdown com o ItemSelector
        selectedItem = (Items)EditorGUILayout.ObjectField("Item:", selectedItem, typeof(Items), false);

        GUILayout.Space(4);

        if (GUILayout.Button("Adicionar ao Inventário"))
        {
            if (selectedItem != null)
            {
                inv.AddItem(selectedItem);
                Debug.Log($"[Inventory] Item adicionado pelo inspector: {selectedItem.itemName}");
            }
            else
            {
                Debug.LogWarning("Nenhum item selecionado!");
            }
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Limpar Inventário"))
        {
            inv.ClearAll();
            Debug.Log("[Inventory] Todos os itens foram removidos.");
        }

        GUILayout.Space(10);
    }
}
#endif
