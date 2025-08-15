using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // References to the Main Menu UI
    public GameObject mainMenu;
    public GameObject startBtn;
    public GameObject continueBtn;
    public GameObject quitBtn;
    public GameObject creditsBtn;

    // References to 'Start Over' screen
    public GameObject startOver;
    public GameObject startBtn2;
    public GameObject cancelBtn;
    
    // References to Credits screen
    public GameObject credits;
    public GameObject returnBtn;

    private int currentLevel;
    
    void Start()
    {
        MouseLocker.UnlockMouse();
        
        // See if there is save data.
        currentLevel = SaveLoad.LoadGame();
        
        // Add listeners for main menu.
        if (currentLevel == 0) {
            continueBtn.SetActive(false);
            startBtn.GetComponent<Button>().onClick.AddListener(StartGame);
        }
        else {
            continueBtn.SetActive(true);
            startBtn.GetComponent<Button>().onClick.AddListener(PromptEraseSaveData);
            continueBtn.GetComponent<Button>().onClick.AddListener(ContinueGame);

            // Add listeners for 'start over' menu.
            startBtn2.GetComponent<Button>().onClick.AddListener(StartGame);
            cancelBtn.GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
        }

        creditsBtn.GetComponent<Button>().onClick.AddListener(OpenCredits);
        quitBtn.GetComponent<Button>().onClick.AddListener(QuitGame);
        
        // Add listeners for credits screen.
        returnBtn.GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
    }

    // Ask the player if they would like to erase save data.
    void PromptEraseSaveData()
    {
        mainMenu.SetActive(false);
        startOver.SetActive(true);
    }
    
    // Leave 'start over' prompt; return to main menu.
    void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        startOver.SetActive(false);
        credits.SetActive(false);
    }
    
    // Open the credits screen.
    void OpenCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Start from level 1.
    void StartGame()
    {
        SaveLoad.SaveGame(1);
        LoadScene(1);
    }

    // Continue game from save data.
    void ContinueGame()
    {
        LoadScene(currentLevel);
    }

    // Load given level.
    void LoadScene(int scene)
    {
        PersistentData.DidPlayerDie = false;
        SceneManager.LoadScene(scene);
    }
    
    // Quit the game.
    void QuitGame()
    {
        Application.Quit();
    }
}
