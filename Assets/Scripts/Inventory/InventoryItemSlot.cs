using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    public Image icon;
    private StorableItem _storableItem;

    public void AddItem(StorableItem newItem)
    {
        _storableItem = newItem;
        icon.sprite = _storableItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        _storableItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
