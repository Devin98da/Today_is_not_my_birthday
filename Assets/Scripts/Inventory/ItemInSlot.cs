using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInSlot : MonoBehaviour
{
    public StorableItem storableItem;
    [SerializeField] private Image _storableItemImage;
    [SerializeField] private TextMeshProUGUI _storableItemStackAmount;

    public void InitializeItem(StorableItem newItem)
    {
        storableItem = newItem;
        _storableItemImage.sprite = newItem.icon;
        RefreshStackableItem(newItem);

    }

    public void RefreshStackableItem(StorableItem newItem)
    {
        _storableItemStackAmount.text = newItem.amount.ToString();
        bool hasStackbles = newItem.amount > 1;
        _storableItemStackAmount.gameObject.SetActive(hasStackbles);
    }
}
