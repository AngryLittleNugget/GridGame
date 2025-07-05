using UnityEngine;

public class Health : Item
{
    private PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    public override bool Interact()
    {
        Debug.Log($"Grabbing {itemName}");
      //  InventoryManagement.Instance.AddItem(itemName, itemHashID);
        playerHealth.HealHealth(1);
        Destroy(this.gameObject);
        return true;

    }

/*
            void Update()
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Debug.Log("Registering input of M");
                    if (InventoryManagement.Instance.inventory.ContainsKey(itemName))
                    {
                        Debug.Log($"{itemName} is not null");
                        if (InventoryManagement.Instance.inventory[itemName] > 0)
                        {
                            UseItem(1);
                            InventoryManagement.Instance.RemoveItem(itemName);
                        }
                    }
                    else
                    {
                        Debug.Log("GogoJuice not in yet");
                        return;
                    }
                }
            }

            public void UseItem(int heal)
            {
                playerHealth.HealHealth(heal);
            }
            */
}
