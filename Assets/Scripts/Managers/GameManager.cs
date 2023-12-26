using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState { MENU, PLAYING, PAUSED, GAMEOVER, LOADING, WIN, STORY, SETTINGS, LEVEL_TRANSITION };
public class GameManager : MonoBehaviour
{
    // Singleton instance to ensure there's only one GameManager
    public static GameManager Instance { get; private set; }


    [SerializeField] private Player _player;
    [SerializeField] private MouseLook _mouseLook;
    [SerializeField] private StoryManager _storyManager;
    // Enum to represent different game states
    //private enum GameState { Menu, Playing, Paused, GameOver }
    [SerializeField] private GameState currentState; // Current state of the game

    // Other variables for player progression, score, etc.
    // Add variables for player progress, score, levels, etc.

    // Awake is called before Start; used to set up the Singleton pattern
    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize game state and other variables
        currentState = GameState.MENU;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for input, update game state, handle events, etc.
        switch (currentState)
        {
                case GameState.MENU:
                    HandleMenuInput();
                    break;

                case GameState.PLAYING:
                    HandlePlayingInput();

                    UpdateGameplay();
                //CheckGameOverConditions();
                break;

                case GameState.PAUSED:
                    HandlePauseInput();
                    break;

                case GameState.GAMEOVER:
                    HandleGameOverInput();
                    break;
                case GameState.WIN:
                    break;
                case GameState.SETTINGS:
                    break;
                case GameState.STORY:
                    _storyManager.DisplayCurrentStoryNode();

                    break;
                case GameState.LOADING: 
                    break;
                case GameState.LEVEL_TRANSITION: 
                    break;
            
        }
    }
    // Add methods for level management, player progression, events, input handling, etc.
    // Define and implement methods for specific functionalities like loading levels, handling player progression, etc.

    // Example method for changing game state
    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
        // Add any additional logic based on the new state
    }

    // Other methods for managing levels, player progression, etc.
    // Implement methods for loading and unloading levels, managing player progression, etc.

    // Example method for handling UI
    public void UpdateUI()
    {
        // Update UI elements based on game state, score, etc.
    }

    // Add other methods as needed for different features
    // Implement methods for features like audio management, saving and loading the game, etc.

    // Example method for saving game
    public void SaveGame()
    {
        // Save relevant game data
    }

    // Example method for loading game
    public void LoadGame()
    {
        // Load relevant game data
    }


    private void HandlePlayingInput()
    {
        // Check for playing-related input
        if (Keyboard.current.escapeKey.wasPressedThisFrame) // Example: Check if the "Pause" button is pressed
        {
            // Pause the game
            ChangeGameState(GameState.PAUSED);
            UIManager.Instance.ShowHidePausePanel(true);

            Time.timeScale = 0;
        }

        // Add more playing-related input checks and actions as needed
    }

    private void UpdateGameplay()
    {
        _player.HandlePlayingInput();
        _mouseLook.HandleMouseLook();
        // Update game elements, handle game events, etc.
        // Example: Move the player, update AI, handle collisions, etc.

        // You might want to organize gameplay logic into separate methods or classes
        // to keep the Update method clean and modular.
    }

    private void HandlePauseInput()
    {
        // Check for pause-related input
        if (Keyboard.current.escapeKey.wasPressedThisFrame) // Example: Check if the "Pause" button is pressed
        {
            // Resume the game
            Time.timeScale = 1;
            UIManager.Instance.ShowHidePausePanel(false);

            ChangeGameState(GameState.PLAYING);
        }
    }

private void HandleMenuInput()
{
    // Check for menu-related input
    if (Input.GetButtonDown("Submit")) // Example: Check if the "Submit" button is pressed
    {
        // Perform action when the user submits in the menu (e.g., start the game)
        StartGame();
    }
    else if (Input.GetButtonDown("Cancel")) // Example: Check if the "Cancel" button is pressed
    {
        // Perform action when the user cancels or goes back in the menu (e.g., quit the game)
        QuitGame();
    }

    // Add more menu-related input checks and actions as needed
}

private void StartGame()
{
    // Code to start the game, e.g., change the game state to Playing
    //GameManager.ChangeGameState(GameState.Playing);
}

private void QuitGame()
{
    // Code to quit the game, e.g., handle application quitting
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the Unity Editor
#else
    Application.Quit(); // Quit the application in a build
#endif
}

    private void CheckGameOverConditions()
    {
        // Check for game over conditions (e.g., player health reaches zero)
        // If game over conditions are met, transition to the game over state
        if (IsGameOver())
        {
            ChangeGameState(GameState.GAMEOVER);
        }
    }

    private bool IsGameOver()
    {
        // Implement your specific game over conditions
        // For example: return true if player health is zero
        // You might also check for other conditions like a timer reaching zero, etc.
        return false;
    }

    private void HandleGameOverInput()
    {
        // Display game over UI, score, options, etc.
        // Example: show a game over screen with score and options to restart or go back to the main menu

        // Check for input related to the game over screen
        if (Input.GetButtonDown("Restart")) // Example: Check if the "Restart" button is pressed
        {
            // Restart the game
            RestartGame();
        }
        else if (Input.GetButtonDown("MainMenu")) // Example: Check if the "Main Menu" button is pressed
        {
            // Return to the main menu
            ReturnToMainMenu();
        }

        // Add more game over screen-related input checks and actions as needed
    }

    private void RestartGame()
    {
        // Code to reset game state, player position, etc., and start a new game
        // For example, you might reload the current level or reset relevant game data
        // ChangeGameState(GameState.Playing);
    }

    private void ReturnToMainMenu()
    {
        // Code to return to the main menu
        // For example, load the main menu scene or reset the game state
        // ChangeGameState(GameState.Menu);
    }
}
