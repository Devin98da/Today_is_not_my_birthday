using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable, IInteractable
{
    public bool CanExamine => throw new System.NotImplementedException();

    public void Interact()
    {
        Debug.Log("Open door");
    }
}
