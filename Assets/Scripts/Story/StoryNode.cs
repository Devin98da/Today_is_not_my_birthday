#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public enum StoryNodeType
{
    CUTSCENE,
    PLAYER_CHOICE,
    CUTSCENE_CHOICE,
    DEFAULT
}

[System.Serializable]
public class StoryNode 
{
    public int storyNodeId;
    public string storyNodeText;
    public List<Choice> choices;
    public StoryNodeType storyNodeType;
    //public bool isCutsceneNode;
    public VideoClip cutsceneClip;
    public int nextStoryNodeIndex;
}





