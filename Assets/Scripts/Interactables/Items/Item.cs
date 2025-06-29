using UnityEngine;

public class Item : Interactable
{
    public string itemName;
    public override bool Interact()
    {
        Debug.Log($"Grabbing {itemName}");
        InventoryManagement.Instance.AddItem(itemName);
        Destroy(this.gameObject);
        return true;
    }
}
