using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : StorableItem
{
    public override void Interact()
    {
        base.Interact();
    }

    public override void Use()
    {
        Debug.Log("Use item " + interactableName);
    }
}
