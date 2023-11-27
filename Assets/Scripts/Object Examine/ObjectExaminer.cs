using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExaminer : MonoBehaviour
{
    [SerializeField] private List<StorableItem> interactablesList = new List<StorableItem>();
    [SerializeField] private GameObject _examineCanvas;


    private void Start()
    {
        StorableItem.OnExamineItem += ExamineObject;
    }

    public void ExamineObject(StorableItem item)
    {
        foreach (var obj in interactablesList)
        {
            if (obj.id == item.id)
            {
                obj.gameObject.SetActive(true);
                Debug.Log("Object now examine" + item.interactableName);
            }
            else
            {
                obj.gameObject.SetActive(false);
                Debug.Log("There are no items in interactable list");

            }
        }
        _examineCanvas.SetActive(true);
    }

    public void OnTakeObject()
    {
        // add item to inventory
    }

    public void OnCanvasGroupChanged()
    {
        // hide examine uI
    }
}
