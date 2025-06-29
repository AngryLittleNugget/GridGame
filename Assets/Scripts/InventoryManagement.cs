using UnityEngine;
using System.Collections.Generic;

public class InventoryManagement : MonoBehaviour
{
    public static InventoryManagement Instance;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (KeyValuePair<string, int> KVP in inventory)
            {
                Debug.Log($"Item: {KVP.Key} x {KVP.Value} ");
            }
        }
    }

    public void AddItem(string itemName)
    {
        Debug.Log("AddItem is firing");
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName]++;
        }
        else
        {
            inventory[itemName] = 1;
            Debug.Log($"{itemName} added to inventory.");
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
