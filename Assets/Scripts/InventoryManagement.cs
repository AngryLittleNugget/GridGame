using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryManagement : MonoBehaviour
{
    public static InventoryManagement Instance;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Dictionary<string, int> pickedUpInThisRoom = new Dictionary<string, int>();
    GameObject inventoryWindow;
    private TextMeshProUGUI inventoryText;
    public HashSet<string> tempHashStorage = new HashSet<string>();
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
        SceneManager.sceneLoaded += ExitClear;
        PlayerHealth.GameOver += RemoveFromInventory;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= ExitClear;
        PlayerHealth.GameOver -= RemoveFromInventory;
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (inventoryWindow.activeInHierarchy == false)
            {
                inventoryWindow.SetActive(true);
                string full_text = "";
                foreach (KeyValuePair<string, int> KVP in pickedUpInThisRoom)
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
        Debug.Log($"{scene.name}, Build Index: {scene.buildIndex}");
        inventoryWindow = GameObject.FindWithTag("UI Inventory");
        /* Transform iWTransform = inventoryWindow.transform;
         inventoryText = iWTransform.Find("InventoryTMP").GetComponent<TextMeshProUGUI>(); */
        inventoryText = inventoryWindow.GetComponentInChildren<TextMeshProUGUI>();
        if (inventoryWindow.activeInHierarchy == true)
        {
            inventoryWindow.SetActive(false);
        }
    }

    public void AddItem(string itemName, string itemHashID, Dictionary<string, int> dictionary)
    {
        Debug.Log("AddItem is firing");
        if (dictionary.ContainsKey(itemName))
        {
            dictionary[itemName]++;
        }
        else
        {
            dictionary[itemName] = 1;
            Debug.Log($"{itemName} added to inventory.");
        }

        tempHashStorage.Add(itemHashID);
        Debug.Log($"{itemHashID} added to {nameof(dictionary)}");
        foreach (KeyValuePair<string, int> KVP in dictionary)
        {
            Debug.Log($"{nameof(dictionary)}: {KVP.Key}, {KVP.Value}");
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

    public void ExitClear(Scene scene, LoadSceneMode mode)
    {
        pickedUpInThisRoom.Clear();
        foreach (string hash in tempHashStorage)
        {
            itemsToSkip.Add(hash);
        }
        tempHashStorage.Clear();
    }

    public void RemoveFromInventory()
    {
        foreach (KeyValuePair<string, int> KVP in InventoryManagement.Instance.pickedUpInThisRoom)
        {
            if (InventoryManagement.Instance.inventory.ContainsKey(KVP.Key))
            {
                InventoryManagement.Instance.RemoveItem(KVP.Key);
            }
        }
        tempHashStorage.Clear();
    }
}
