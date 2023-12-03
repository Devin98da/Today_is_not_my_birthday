using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<StorableItem> inventory = new List<StorableItem>();
    public List<StorableItem> items;
    public List<StorableItem> notes;
    public List<StorableItem> documents;

    private void Awake()
    {
        ObjectExaminer.OnPickUpExamineItem += AddItem;
    }

    public void AddItem(StorableItem item)
    {
        if (item.isStackble)
        {
            bool _isItemExistsInInventoty = false;

            foreach (var inventoryItem in inventory.ToArray())
            {
                if (inventoryItem.interactableName == item.interactableName)
                {
                    _isItemExistsInInventoty = true;
                    inventoryItem.amount += item.amount;
                    if (inventoryItem.maxAmount > inventoryItem.amount)
                    {
                        AddItemToInventory(inventoryItem);

                    }
                    // increase item amount

                }
            }

            if (!_isItemExistsInInventoty)
            {
                AddItemToInventory(item);
            }
        }
        else
        {
            AddItemToInventory(item);
        }
        // check inentory has this item
        // if not add it and 
    }

    public void AddItemToInventory(StorableItem item)
    {
        inventory.Add(item);
    }

    public void RemoveItem()
    {

    }

    /// <summary>
    /// Display items in UI
    /// </summary>
    public void ShowItems()
    {

    }

    /// <summary>
    /// Display notes in UI
    /// </summary>
    public void ShowNotes()
    {
        
    }

    /// <summary>
    /// Display documents in UI
    /// </summary>
    public void ShowDocuments()
    {

    }
}
