using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    ITEM, NOTE, DOCUMENT, NON_STORABLE
}

public class StorableItem : Interactable, IInteractable
{
    public bool isStackble;
    public Sprite icon;
    public int maxStackSize;
    public int amount;
    public int maxAmount;
    public ItemType type;

    public static event Action<StorableItem> OnExamineItem;
    public static event Action<StorableItem> OnExamineDocs;


    public bool CanExamine => throw new System.NotImplementedException();

    public virtual void Interact()
    {
        switch(type)
        {
            case ItemType.ITEM:
                OnExamineItem?.Invoke(this);
                break;
            case ItemType.NOTE:
                OnExamineDocs?.Invoke(this);
                break;
            case ItemType.DOCUMENT:
                OnExamineDocs?.Invoke(this);
                break;
            default:
                break;
        }
    }

    public virtual void Use()
    {

    }

    public virtual void Examine()
    {
        OnExamineItem?.Invoke(this);

    }
}
