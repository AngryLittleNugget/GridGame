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
        InventoryManagement.Instance.AddItem(itemName, itemHashID, InventoryManagement.Instance.inventory);
        InventoryManagement.Instance.AddItem(itemName, itemHashID, InventoryManagement.Instance.pickedUpInThisRoom);
        Destroy(this.gameObject);
        return true;
    }
}
