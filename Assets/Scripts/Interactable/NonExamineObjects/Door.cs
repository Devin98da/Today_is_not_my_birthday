using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable, IInteractable
{
    public bool CanExamine => throw new System.NotImplementedException();
    public StoryManager storyManager;
    

    public void Interact()
    {
        Debug.Log("Open door");
        if(hasStoryIndex && storyNodeIndex == storyManager.GetCurrentStoryNodeIndex())
        {
            //uiManager.ShowStoryNode();
            storyManager.DisplayCurrentStoryNode();
        }
    }
}
