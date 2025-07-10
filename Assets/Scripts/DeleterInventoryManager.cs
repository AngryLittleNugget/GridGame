using UnityEngine;

public class DeleterInventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (InventoryManagement.Instance != null)
        {
            Destroy(InventoryManagement.Instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
