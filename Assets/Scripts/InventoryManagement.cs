using UnityEngine;
using System.Collections.Generic;

public class InventoryManagement : MonoBehaviour
{
    public static InventoryManagement Instance;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddItem(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName]++;
        }
        else
        {
            inventory[itemName] = 1;
        }
    }
    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName) && inventory[itemName] > 0;
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName]--;
            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
        }
    }
}
