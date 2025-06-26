using UnityEngine;

public class LockedDoor : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Interact()
    {
        if (InventoryManagement.Instance.HasItem("Key"))
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
