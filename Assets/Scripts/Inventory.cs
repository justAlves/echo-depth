using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    // O inventário real (runtime)
    private HashSet<string> items = new HashSet<string>();

    // Lista apenas para visualização no inspector
    [SerializeField] private List<Items> debugItems = new List<Items>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(Items item)
    {
        if (item == null) return;

        items.Add(item.itemId);

        // atualiza visão no inspector
        if (!debugItems.Contains(item))
            debugItems.Add(item);
    }

    public bool HasItem(string itemId)
    {
        return items.Contains(itemId);
    }

    public void ClearAll()
    {
        items.Clear();
        debugItems.Clear();
    }
}
