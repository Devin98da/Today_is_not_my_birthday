using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExaminer : MonoBehaviour
{
    [SerializeField] private List<Interactable> interactablesList = new List<Interactable>();
    [SerializeField] private GameObject _examineCanvas;


    private void Start()
    {
        Player.OnExamineItem += ExamineObject;
    }

    public void ExamineObject(IInteractable interactable)
    {
        //foreach (var obj in interactablesList)
        //{
        //    if(obj === interactable)
        //    {
        //        obj.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        obj.gameObject.SetActive(false);
        //    }
        //}
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
