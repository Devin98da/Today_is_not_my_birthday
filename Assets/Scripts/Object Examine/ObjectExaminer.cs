using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectExaminer : MonoBehaviour
{
    [SerializeField] private List<StorableItem> interactablesList = new List<StorableItem>();
    [SerializeField] private GameObject _examineCanvas;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;

    StorableItem _storableItem;

    public static event Action<StorableItem> OnPickUpExamineItem;


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
            OnZoomExamineObject();
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
            //_player.AddItem(_storableItem);
            _examineCanvas.SetActive(false);
            OnPickUpExamineItem?.Invoke(_storableItem);

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
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            _camera.fieldOfView = 50;
        }else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            _camera.fieldOfView = 60;
            
        }
    }
}
