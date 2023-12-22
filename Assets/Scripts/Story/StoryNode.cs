using UnityEditor;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;
using UnityEngine.Video;

public enum StoryNodeType
{
    CUTSCENE,
    PLAYER_CHOICE,
    CUTSCENE_CHOICE,
    DEFAULT,
    DIALOGUE    
}

[System.Serializable]
public class StoryNode 
{
    public int storyNodeId;
    public string storyNodeText;
    public int nextStoryNodeIndex;
    public StoryNodeType storyNodeType;
    public List<Choice> choices;
    //public bool isCutsceneNode;

    public VideoClip cutsceneClip;
    public AudioClip dialogueClip;
}







