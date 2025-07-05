using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManagement : MonoBehaviour
{
    public static InventoryManagement Instance;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    GameObject inventoryWindow;
    private TextMeshProUGUI inventoryText;
    public HashSet<string> itemsToSkip = new HashSet<string>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("Inventory manager destroyed in awake");
        }
        
        Debug.Log($"Inventory text null: {inventoryText == null}");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (inventoryWindow.activeInHierarchy == false)
            {
                inventoryWindow.SetActive(true);
                string full_text = "";
                foreach (KeyValuePair<string, int> KVP in inventory)
                {
                    full_text += $"Item: {KVP.Key} x {KVP.Value} \n";
                }
                inventoryText.text = full_text;
            }
            else if (inventoryWindow.activeInHierarchy == true)
            {
                inventoryWindow.SetActive(false);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
              inventoryWindow = GameObject.FindWithTag("UI Inventory");
        /* Transform iWTransform = inventoryWindow.transform;
         inventoryText = iWTransform.Find("InventoryTMP").GetComponent<TextMeshProUGUI>(); */
        inventoryText = inventoryWindow.GetComponentInChildren<TextMeshProUGUI>();
        if (inventoryWindow.activeInHierarchy == true)
        {
            inventoryWindow.SetActive(false);
        }
    }

    public void AddItem(string itemName, string itemHashID)
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

        itemsToSkip.Add(itemHashID);
        Debug.Log($"{itemHashID} added to list");
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
