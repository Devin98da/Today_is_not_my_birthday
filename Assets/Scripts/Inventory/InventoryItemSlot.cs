using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    public Image icon;
    [SerializeField] private Color _selectedColor, _nonSelectedColor;

    private void Awake()
    {
        DeselectItem();
    }

    public void SelectItem()
    {
        icon.color = _selectedColor;
    }

    public void DeselectItem()
    {
        icon.color = _nonSelectedColor;
    }
}
