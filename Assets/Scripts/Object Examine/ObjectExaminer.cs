using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectExaminer : MonoBehaviour
{
    [SerializeField] private List<StorableItem> examineItemList = new List<StorableItem>();
    [SerializeField] private List<StorableItem> examineDocsList = new List<StorableItem>();
    [SerializeField] private GameObject _examineCanvas;
    [SerializeField] private GameObject _examineItemUi, _examineDocsUI;
    [SerializeField] private GameObject _documentInfoPanel;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;

    StorableItem _storableItem;

    public static event Action<StorableItem> OnPickUpExamineItem;


    private void Start()
    {
        StorableItem.OnExamineItem += ExamineItemObjects;
        StorableItem.OnExamineDocs += ExamineDocsObjects;
        ExamineDocument.OnExamineDocument += ExamineDocumentInfo;
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

    public void ExamineItemObjects(StorableItem item)
    {
        _examineDocsUI.SetActive(false);
        _documentInfoPanel.SetActive(false);
        _storableItem = item;
        // not walatath me wage loop karala adala note/doc eka pennanna one
        foreach (var obj in examineItemList)
        {
            if (obj.id == item.id)
            {
                obj.gameObject.SetActive(true);
                //Debug.Log("Object now examine" + item.interactableName);
            }
            else
            {
                obj.gameObject.SetActive(false);
                //Debug.Log("There are no items in interactable list");

            }
        }
        _examineItemUi.SetActive(true);
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
            //_examineItemUi?.SetActive(false);
            //_examineDocsUI.SetActive(false);
            //_documentInfoPanel.SetActive(false);

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

    //======================================================DOCS==================================
    public void ExamineDocsObjects(StorableItem item)
    {
        _examineItemUi?.SetActive(false);


        _storableItem = item;
        // not walatath me wage loop karala adala note/doc eka pennanna one
        foreach (var obj in examineDocsList)
        {
            if (obj.id == item.id)
            {
                obj.gameObject.SetActive(true);
                //Debug.Log("Object now examine" + item.interactableName);
            }
            else
            {
                obj.gameObject.SetActive(false);
                //Debug.Log("There are no items in interactable list");

            }
        }
        _examineDocsUI.SetActive(true);

        _examineCanvas.SetActive(true);
    }

    void ExamineDocumentInfo()
    {
        _documentInfoPanel.SetActive(true);
    }

}
