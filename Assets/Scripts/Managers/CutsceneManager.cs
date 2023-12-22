using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] private RawImage _cutsceneRawImage;
    [SerializeField] private StoryManager _storyManager;

    void Start()
    {
        // Subscribe to the videoPlayer's completion event
        videoPlayer.loopPointReached += OnCutsceneFinished;
    }

    public void PlayCutscene(VideoClip cutscene)
    {
        _cutsceneRawImage.gameObject.SetActive(true);
        videoPlayer.clip = cutscene;
        videoPlayer.Play();
    }

    void OnCutsceneFinished(VideoPlayer vp)
    {
        // Do something when the cutscene finishes
        // if after cutscene there are player choices called it here
        Debug.Log("Cutscene finished!");
        _cutsceneRawImage.gameObject.SetActive(false);
        StoryNode storyNode = _storyManager.GetCurrentStoryNode();

        switch(storyNode.storyNodeType)
        {
            case StoryNodeType.CUTSCENE_CHOICE:
                _storyManager.DisplayCurrentStoryNode();
                break;
            case StoryNodeType.DIALOGUE:
                //_storyManager.DisplayCurrentStoryNode();
                break;
            default:
                break;
        }

    }
}
