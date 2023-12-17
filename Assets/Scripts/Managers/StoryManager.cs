using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public List<StoryNode> storyNodes;
    [SerializeField]private int currentNodeIndex = 0;

    private int currentNodeId;

    private void Start()
    {
        //DisplayCurrentStoryNode();
    }

    public string GetCurrentNodeText()
    {
        return storyNodes[currentNodeIndex].storyNodeText;
    }

    public StoryNode GetCurrentStoryNode()
    {
        return storyNodes[currentNodeIndex];
    }

    public List<Choice> GetCurrentNodeChoices()
    {
        return storyNodes[currentNodeIndex].choices;
    }

    void DisplayCurrentStoryNode()
    {
        if(currentNodeIndex < storyNodes.Count)
        {
            StoryNode storyNode = storyNodes[currentNodeIndex];
            //Debug.Log(storyNode.storyNodeText);
            for(int i = 0;i<storyNode.choices.Count;i++)
            {
                //Debug.Log(i + 1 + storyNode.choices[i].choiceText);
            }
        }
        else
        {
            Debug.Log("Enf of the story");
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if(currentNodeIndex < storyNodes.Count)
        {
            StoryNode currentNode = storyNodes[currentNodeIndex];
            if(choiceIndex >= 0 && choiceIndex < currentNode.choices.Count)
            {
                //if(currentNode.isCutsceneNode)
                //{
                //    // play cutscene
                //    // CutsceneManger.PlayCutscene(cutSceneId);
                //}
                //else
                //{
                //    // show/ hide relavant collectables based on choices
                //    // change objectives based on choices
                //}
                currentNodeIndex = currentNode.choices[choiceIndex].nextStoryNode;
                DisplayCurrentStoryNode();
            }
            else
            {
                Debug.Log("Invalid choice index");
            }
        }
    }

}
