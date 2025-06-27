using UnityEngine;

public class LockedDoor : Interactable
{
    private GridManager gridManager;
    public string itemNeeded;
    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Interact()
    {
        if (InventoryManagement.Instance.HasItem(itemNeeded))
        {
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
}
