#if UNITY_EDITOR
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemSelectorAttribute))]
public class ItemSelectorDrawer : PropertyDrawer
{
    private Items[] items;
    private string[] displayNames;

    private void LoadItems()
    {
        string[] guids = AssetDatabase.FindAssets("t:Items");
        items = new Items[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            items[i] = AssetDatabase.LoadAssetAtPath<Items>(path);
        }

        System.Array.Sort(items, (a, b) => a.itemName.CompareTo(b.itemName));

        displayNames = new string[items.Length + 1];
        displayNames[0] = "Nenhum";

        for (int i = 0; i < items.Length; i++)
        {
            displayNames[i + 1] = $"{items[i].itemName} [{items[i].itemId}]";
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (items == null)
            LoadItems();

        EditorGUI.BeginProperty(position, label, property);

        // Label
        Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        EditorGUI.LabelField(labelRect, label);

        // Dropdown
        float fieldWidth = position.width - EditorGUIUtility.labelWidth - 50f;
        Rect popupRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, fieldWidth, position.height);

        // Icon preview
        Rect iconRect = new Rect(
            popupRect.x + popupRect.width + 4,
            popupRect.y + 2,
            18, 18
        );

        Items current = property.objectReferenceValue as Items;

        int selectedIndex = 0;

        if (current != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == current)
                {
                    selectedIndex = i + 1;
                    break;
                }
            }
        }

        int newIndex = EditorGUI.Popup(popupRect, selectedIndex, displayNames);

        if (newIndex == 0)
            property.objectReferenceValue = null;
        else
            property.objectReferenceValue = items[newIndex - 1];

        // Draw icon
        if (current != null && current.itemIcon != null)
        {
            GUI.DrawTexture(iconRect, current.itemIcon.texture, ScaleMode.ScaleToFit);
        }
        else
        {
            EditorGUI.DrawRect(iconRect, new Color(0, 0, 0, 0.3f));
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight + 2;
    }
}
#endif
