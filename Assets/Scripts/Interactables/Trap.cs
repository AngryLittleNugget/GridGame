using UnityEngine;

public class Trap : Interactable
{
    public override bool Interact()
    {
        PlayerHealth pHealth = FindFirstObjectByType<PlayerHealth>();
        pHealth.TakeDamage(1);
        return false;
    }
}
