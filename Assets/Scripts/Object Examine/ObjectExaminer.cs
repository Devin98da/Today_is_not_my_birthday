using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectExaminer : MonoBehaviour
{
    [SerializeField] private List<StorableItem> interactablesList = new List<StorableItem>();
    [SerializeField] private GameObject _examineCanvas;
    [SerializeField] private Player _player;

    StorableItem _storableItem;


    private void Start()
    {
        StorableItem.OnExamineItem += ExamineObject;
    }

    private void Update()
    {
        if (_examineCanvas.activeSelf)
        {
            OnExamineTakeObject();
            OnExitExamine();
        }
    }

    public void ExamineObject(StorableItem item)
    {
        _storableItem = item;
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

    public void OnExamineTakeObject()
    {
        // add item to inventory
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            _player.AddItem(_storableItem);
            _examineCanvas.SetActive(false);
        }
    }

    public void OnExitExamine()
    {
        // hide examine uI
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _examineCanvas.SetActive(false);
            _storableItem.gameObject.SetActive(true);
        }

    }

    public void OnZoomExamineObject()
    {

    }
}
