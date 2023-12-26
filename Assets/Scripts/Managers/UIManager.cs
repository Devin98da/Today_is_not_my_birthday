using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {
        get
        {
            return _instance;
        }
    }
    private static UIManager _instance;

    [SerializeField] private StoryManager _storyManager;
    [SerializeField] private TextMeshProUGUI _currentStoryNodeText;
    [SerializeField] private Button[] _choicesButtons;
    [SerializeField] private GameObject _storyPanel;
    [SerializeField] private GameObject _pausePanel;

    private void Start()
    {
        //UpdateStoryUI();
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // -------------------------------------------- SHOW/ HIDE PAUSE PANEL ---------------------------------------------------------
    public void ShowHidePausePanel(bool pause)
    {
        _pausePanel.SetActive(pause);
    }

    // -------------------------------------------- SHOW STORY NODE ---------------------------------------------------------
    public void ShowStoryNode()
    {
        _storyPanel.SetActive(true);
        UpdateStoryUI();
    }

    void UpdateStoryUI()
    {
        //_currentStoryNodeText.text = _storyManager.GetCurrentNodeText();

        for(int i=0; i < _storyManager.GetCurrentNodeChoices().Count; i++)
        {
            //if (i < _storyManager.GetCurrentNodeChoices().Count)
            //{
            //Debug.Log(_storyManager.GetCurrentNodeChoices()[1].choiceText);
            //Debug.Log(_choicesButtons[i].GetComponentInChildren<TextMeshProUGUI>().text);
                _choicesButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _storyManager.GetCurrentNodeChoices()[i].choiceText;
                _choicesButtons[i].gameObject.SetActive(true);
                int choiceIndex = i;
                _choicesButtons[i].onClick.RemoveAllListeners();
                _choicesButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            //}
            //else
            //{
            //    _choicesButtons[i].gameObject.SetActive(false);
            //}
        }

    }

    void OnChoiceSelected(int choiceIndex)
    {
        _storyManager.MakeChoice(choiceIndex);
        UpdateStoryUI();
        _storyPanel.SetActive(false);
    }

    
}
