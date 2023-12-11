using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public List<StorableItem> inventory = new List<StorableItem>();
    public List<StorableItem> items;
    public List<StorableItem> notes;
    public List<StorableItem> documents;

    [Header("Inventory UI")]
    [SerializeField] private GameObject inventoryUI;
    //[SerializeField] private List<StorableItem> inventoryItemsSlots;
    [SerializeField] private InventoryItemSlot[] inventoryItemSlots, inventoryDocumentsSlots, inventoryNotesSlots;
    [SerializeField] private GameObject[] inventoryUIs;
    [SerializeField] private int _currentShowingUI = 0;
    [SerializeField] private GameObject _itemInInventoryPrefab;
    [SerializeField] private int _selectedItemSlot, _selectedDocSlot,_selectedNoteSlot;
    [SerializeField] private int _itemSlotChange, _ndSlotChange;
    [SerializeField] private int _itemSlotCount, _noteSlotsCount, _documentSlotsCount;

    // Dictionary to map _currentShowingUI values to inventory slot arrays
    [SerializeField] private Dictionary<int, InventoryItemSlot[]> inventorySlotsDictionary;

    private void Start()
    {
        inventorySlotsDictionary = new Dictionary<int, InventoryItemSlot[]>
        {
            { 0, inventoryItemSlots },
            { 1, inventoryDocumentsSlots },
            { 2, inventoryNotesSlots }
        };
    }
    private void Awake()
    {
        ObjectExaminer.OnPickUpExamineItem += AddItem;
    }

    private void Update()
    {
        OpenCloseInventory();
        UseSelectedInventoryItem();
        ExamineSelectedInventoryItem();
        ChangeSelectedSlotUsingWASDKeys();
    }

    private void OpenCloseInventory()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            // open inventory
            _currentShowingUI = 0;
            //_selectedSlot = 0;
            //DeselectItem();
            //ChangeSelectedSlot(_selectedItemSlot);
            ShowInventoryUI(_currentShowingUI);
            //inventoryUIs[_currentShowingUI].SetActive(true);

            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void AddItem(StorableItem item)
    {
        if (item.isStackble)
        {
            bool _isItemExistsInInventoty = false;

            foreach (var inventoryItem in inventory.ToArray())
            {
                if (inventoryItem.interactableName == item.interactableName && item.type == ItemType.NOTE)
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
            Debug.Log(item.interactableName);
        }
        // check inentory has this item
        // if not add it and 
    }

    void AddItemToInventory(StorableItem item)
    {
        switch(item.type)
        {
            case ItemType.ITEM:
                AddItemBasedOnType(item, inventoryItemSlots);
                break;
            case ItemType.DOCUMENT:
                AddItemBasedOnType(item, inventoryDocumentsSlots);
                break;
            case ItemType.NOTE:
                AddItemBasedOnType(item, inventoryNotesSlots);
                break;
            default:
                break;
        }
    }

    public bool AddItemBasedOnType(StorableItem item, InventoryItemSlot[] slots )
    {
        for(int i = 0; i < slots.Length; i++)
        {
            InventoryItemSlot slot = slots[i];
            ItemInSlot itemInSlot = slot.GetComponentInChildren<ItemInSlot>();

            if(itemInSlot != null)
            {
                //return true;
                if(itemInSlot.storableItem.interactableName == item.interactableName)
                {
                    itemInSlot.RefreshStackableItem(item);
                    return true;
                }
            }
            else
            {
                SpawnItemOnUI(item, slot);
                return true;
            }
        }

        return false;

    }

    private void SpawnItemOnUI(StorableItem storableItem, InventoryItemSlot inventorySlot)
    {
        GameObject newItem = Instantiate(_itemInInventoryPrefab, inventorySlot.transform);
        ItemInSlot itemInSlot = newItem.GetComponent<ItemInSlot>();
        itemInSlot?.InitializeItem(storableItem);
        switch (storableItem.type)
        {
            case ItemType.ITEM:
                items.Add(storableItem);
                break;
            case ItemType.DOCUMENT:
                documents.Add(storableItem);
                break;
            case ItemType.NOTE:
                notes.Add(storableItem);
                break;
            default: break;
        }
        inventory.Add(storableItem);

    }

    private void ClearInventoryUI(InventoryItemSlot[] slots)
    {
        for (int i = 0;i < slots.Length; i++)
        {
            ItemInSlot itemInSlot = slots[i].GetComponentInChildren<ItemInSlot>();
            if(itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);

            }
        }
    }

    public void RemoveItem()
    {

    }

    private void SpawnItemsOnUIBasedOnType(List<StorableItem> items)
    {
        foreach (var item in items)
        {
            for (int i = 0; i < inventoryItemSlots.Length; i++)
            {
                InventoryItemSlot slot = inventoryItemSlots[i];
                ItemInSlot itemInSlot = slot.GetComponentInChildren<ItemInSlot>();

                if (itemInSlot == null)
                {
                    //SpawnItemOnUI(item, slot);
                    GameObject newItem = Instantiate(_itemInInventoryPrefab, slot.transform);
                    ItemInSlot slotItem = newItem.GetComponent<ItemInSlot>();
                    slotItem?.InitializeItem(item);
                    return;
                }


            }
        }
    }

    // SHOW ITEMS ON UI WHEN CLICKED INVENTORY BUTTONS
    public void ShowInventoryUI(int uiNum)
    {
        for (int i=0; i < inventoryUIs.Length;i++)
        {
            if(i == uiNum)
            {
                inventoryUIs[i].SetActive(true);
                _currentShowingUI = uiNum;
            }
            else
            {
                inventoryUIs[i].SetActive(false);
            }
        }
    }

    // CHANGE SELECTED SLOT WHEN CLICK ON IT, USE ON CLICK FUNCTION OF INVENTORY SLOT
    //public void ChangeSelectedSlot(int slot)
    //{
    //    switch (_currentShowingUI)
    //    {
    //        case 0:
    //            SelectSlotFromInvenorty(slot, inventoryItemSlots);
    //            break;
    //        case 1:
    //            SelectSlotFromInvenorty(slot, inventoryDocumentsSlots);
    //            break;
    //        case 2:
    //            SelectSlotFromInvenorty(slot, inventoryNotesSlots);
    //            break;
    //        default:
    //            break;
    //    }
    //    Debug.Log(slot);

    //}

    //private void SelectSlotFromInvenorty(int slot, InventoryItemSlot[] inventorySlots)
    //{
    //    if (_selectedItemSlot >= 0 || _selectedDocSlot >=0 || _selectedNoteSlot >=0)
    //    {
    //        switch (_currentShowingUI)
    //        {
    //            case 0:
    //                inventorySlots[_selectedItemSlot].DeselectItem();
    //                break;
    //            case 1:
    //                inventorySlots[_selectedDocSlot].DeselectItem();
    //                break;
    //            case 2:
    //                inventorySlots[_selectedNoteSlot].DeselectItem();
    //                break;
    //            default:
    //                break;
    //        }

    //    }

    //    switch (_currentShowingUI)
    //    {
    //        case 0:
    //            _selectedItemSlot = slot;
    //            inventorySlots[_selectedItemSlot].Selectitem();

    //            break;
    //        case 1:
    //            _selectedDocSlot = slot;
    //            inventorySlots[_selectedDocSlot].Selectitem();


    //            break;
    //        case 2:
    //            _selectedNoteSlot = slot;
    //            inventorySlots[_selectedNoteSlot].Selectitem();


    //            break;
    //        default:
    //            break;
    //    }
    //    Debug.Log(slot);
    //}


    public void ChangeSelectedSlot(int slot)
    {
        if (_currentShowingUI >= 0 && _currentShowingUI < inventorySlotsDictionary.Count)
        {
            SelectSlotFromInventory(slot, inventorySlotsDictionary[_currentShowingUI]);
        }
    }

    private void SelectSlotFromInventory(int slot, InventoryItemSlot[] inventorySlots)
    {
        DeselectCurrentItemSlot(inventorySlots);

        switch (_currentShowingUI)
        {
            case 0:
                _selectedItemSlot = slot;
                break;
            case 1:
                _selectedDocSlot = slot;
                break;
            case 2:
                _selectedNoteSlot = slot;
                break;
            default:
                break;
        }

        inventorySlots[slot].SelectItem();
        Debug.Log(slot);
    }

    private void DeselectCurrentItemSlot(InventoryItemSlot[] inventorySlots)
    {
        int selectedSlot = GetSelectedSlot();
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].DeselectItem();
        }
    }

    private int GetSelectedSlot()
    {
        switch (_currentShowingUI)
        {
            case 0:
                return _selectedItemSlot;
            case 1:
                return _selectedDocSlot;
            case 2:
                return _selectedNoteSlot;
            default:
                return -1;
        }
    }






    // CHANGE SELECTED SLOT BY USIGN WASD KEYS
    public void ChangeSelectedSlotUsingWASDKeys()
    {
        // TODO => Check an inventory item is available tha slot before select using WASD keys
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (_selectedItemSlot >= 5)
            {
                inventoryItemSlots[_selectedItemSlot].DeselectItem();

                _selectedItemSlot -= _itemSlotChange;
                inventoryItemSlots[_selectedItemSlot].SelectItem();
            }
            // documents, items, notes work
            // selected slot + row slot cound -> items
            // slected slot + 1 -> notes & documents


        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (_selectedItemSlot <= 14)
            {
                inventoryItemSlots[_selectedItemSlot].DeselectItem();
                _selectedItemSlot += _itemSlotChange;
                inventoryItemSlots[_selectedItemSlot].SelectItem();
            }
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            inventoryItemSlots[_selectedItemSlot].DeselectItem();
            // documents, items, notes work
            // selected slot + row slot cound -> items
            // slected slot + 1 -> notes & documents
            _selectedItemSlot -= _ndSlotChange;
            if (_selectedItemSlot < 0)
            {
                _selectedItemSlot = 0;
            }
            inventoryItemSlots[_selectedItemSlot].SelectItem();
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            inventoryItemSlots[_selectedItemSlot].DeselectItem();

            _selectedItemSlot += _ndSlotChange;
            if (_selectedItemSlot > _itemSlotCount - 1)
            {
                _selectedItemSlot = _itemSlotCount - 1;
            }
            inventoryItemSlots[_selectedItemSlot].SelectItem();

        }
    }

    // DESELECT INVENTORY ITEM
    public void DeselectItem()
    {
        //inventoryItemSlots[_selectedSlot].DeselectItem();
    }

    // USE SELECTED ITEM
    public void UseSelectedInventoryItem()
    {
        //TO DO -> check inventory is open
        if(Keyboard.current.uKey.wasPressedThisFrame)
        {
            // check item in slot is an item
            //InventoryItemSlot slot = inventoryItemSlots[_selectedSlot];
            //ItemInSlot itemInSlot = slot.GetComponentInChildren<ItemInSlot>();
            //itemInSlot?.storableItem.Use();
            //slot.DeselectItem();
        }
    }

    public void ExamineSelectedInventoryItem()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            //InventoryItemSlot slot = inventoryItemSlots[_selectedSlot];
            //ItemInSlot itemInSlot = slot.GetComponentInChildren<ItemInSlot>();
            //itemInSlot?.storableItem.Examine();
            //slot.DeselectItem();
        }
    }

    /// <summary>
    /// Display items in UI
    /// </summary>
    /// 
    public void ShowItems()
    {
        ClearInventoryUI(inventoryItemSlots);
        //items = inventory.FindAll(i => i.type == ItemType.ITEM);

        SpawnItemsOnUIBasedOnType(items);
    }



    /// <summary>
    /// Display notes in UI
    /// </summary>
    public void ShowNotes()
    {
        ClearInventoryUI(inventoryNotesSlots);
        //notes = inventory.FindAll(i => i.type == ItemType.NOTE);

        SpawnItemsOnUIBasedOnType(notes);

    }

    /// <summary>
    /// Display documents in UI
    /// </summary>
    public void ShowDocuments()
    {
        ClearInventoryUI(inventoryDocumentsSlots);
        //documents = inventory.FindAll(i => i.type == ItemType.DOCUMENT);

        SpawnItemsOnUIBasedOnType(documents);

    }

    private void ShowInventoryItems()
    {
        foreach (var slots in inventoryItemSlots)
        {

        }
    }
}
