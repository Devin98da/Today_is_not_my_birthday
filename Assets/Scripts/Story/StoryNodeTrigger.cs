using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNodeTrigger : MonoBehaviour
{
    public int storyNodeIndex;
    private StoryManager _storyManager;

    private void Start()
    {
        _storyManager = GameObject.FindAnyObjectByType<StoryManager>();
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("Player"))
    //    {
    //        if (_storyManager.GetCurrentStoryNodeIndex() == storyNodeIndex)
    //        {
    //            _storyManager.DisplayCurrentStoryNode();
    //            this.gameObject.SetActive(false);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_storyManager.GetCurrentStoryNodeIndex() == storyNodeIndex)
            {
                _storyManager.DisplayCurrentStoryNode();
                this.gameObject.SetActive(false);
            }
        }
    }
}
