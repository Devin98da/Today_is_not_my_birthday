using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Subscribe to the videoPlayer's completion event
        videoPlayer.loopPointReached += OnCutsceneFinished;
    }

    public void PlayCutscene(VideoClip cutscene)
    {
        videoPlayer.clip = cutscene;
        videoPlayer.Play();
    }

    void OnCutsceneFinished(VideoPlayer vp)
    {
        // Do something when the cutscene finishes
        // if after cutscene there are player choices called it here
        Debug.Log("Cutscene finished!");
    }
}
