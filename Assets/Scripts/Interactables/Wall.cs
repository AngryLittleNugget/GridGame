using UnityEngine;

public class Wall : Interactable
{
  
    public override bool Interact()
    {
        Debug.Log("Fwump");
        return false;
    }
}
