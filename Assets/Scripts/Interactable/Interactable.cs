using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string id;
    public bool hasStoryIndex;
    public int storyNodeIndex;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    //public string id;

    //[ContextMenu("Generate guid for id")]
    //private void GenerateGuid()
    //{
    //    id = System.Guid.NewGuid().ToString();
    //}

    public string interactableName;
    public bool canExamine;
    protected UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

}
