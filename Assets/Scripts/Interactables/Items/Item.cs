using UnityEngine;

public class Item : Interactable
{
    public string itemName;
    public string itemHashID;

    void Start()
    {
     
    }
    public override bool Interact()
    {
        Debug.Log($"Grabbing {itemName}");
        InventoryManagement.Instance.AddItem(itemName, itemHashID);
        Destroy(this.gameObject);
        return true;
    }
}
