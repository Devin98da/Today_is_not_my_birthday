using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // SET CURRENT STORY NODE
    public void SetCurrentStoryNodeIndex(int index)
    {
        currentNodeIndex = index;
    }

    // GET CURRENT STORY NODE INDEX
    public int GetCurrentStoryNodeIndex()
    {
        return currentNodeIndex;
    }
    // GET CURRENT STORY NODE TEXT
    public string GetCurrentNodeText()
    {
        return storyNodes[currentNodeIndex].storyNodeText;
    }

    // GET CURRENT STORY NODE
    public StoryNode GetCurrentStoryNode()
    {
        return storyNodes[currentNodeIndex];
    }

    // GET CURRENT STORY NODE CHOICES
    public List<Choice> GetCurrentNodeChoices()
    {
        return storyNodes[currentNodeIndex].choices;
    }

    // DISPLAY CURRNET STORY NODE
    public void DisplayCurrentStoryNode()
    {
        if(currentNodeIndex < storyNodes.Count)
        {
            StoryNode storyNode = storyNodes[currentNodeIndex];
            switch (storyNode.storyNodeType)
            {
                case StoryNodeType.DEFAULT:
                    break;
                case StoryNodeType.CUTSCENE:
                    // play cutscene
                    // set current story node to next one 
                    
                    // CutsceneManager.PlayCutscene(storyNode.cutscneClip);
                    // currentStoryNodeIndex++;

                    // if after cutscene there are player choices then called next StoryNodeHere
                    
                    break;
                case StoryNodeType.PLAYER_CHOICE:
                    // pause game 
                    // display player choices
                    // after make choice set current story node to next one 
                    break;
                default:
                    break;
            }
            //Debug.Log(storyNode.storyNodeText);
            for(int i = 0;i<storyNode.choices.Count;i++)
            {
                //Debug.Log(i + 1 + storyNode.choices[i].choiceText);
            }
            Debug.Log("Enf of the story");

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
