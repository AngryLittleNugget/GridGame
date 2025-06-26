using UnityEngine;

public class Key : Interactable
{
    public string keyName = "Key";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override bool Interact()
    {
        Debug.Log("Grabbing Key");
        InventoryManagement.Instance.AddItem("Key");
        Destroy(this.gameObject);
        return true;
    }
}
