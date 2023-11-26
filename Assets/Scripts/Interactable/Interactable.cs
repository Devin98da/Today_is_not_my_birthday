using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public bool canExamine;


    public bool CanExamine => canExamine;

    #region Interact
    /// <summary>
    /// Interact with objects
    /// </summary>

    public void Interact()
    {
        Debug.Log("Inteact with object");
    }
    #endregion
}
