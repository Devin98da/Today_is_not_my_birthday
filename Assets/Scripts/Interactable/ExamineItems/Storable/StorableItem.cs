using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableItem : Interactable, IInteractable
{
    public bool isStackble;
    public Sprite icon;
    public int maxStackSize;
    public int amount;
    public int maxAmount;

    public static event Action<StorableItem> OnExamineItem;


    public bool CanExamine => throw new System.NotImplementedException();

    public virtual void Interact()
    {
        OnExamineItem?.Invoke(this);
    }

    public virtual void Use()
    {

    }
}
