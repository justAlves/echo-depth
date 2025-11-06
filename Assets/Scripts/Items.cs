using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item  ")]
public class Items : ScriptableObject
{
    public string itemId;

    public string itemName;

    public Sprite itemIcon;
}
