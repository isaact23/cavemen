/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuOld : MonoBehaviour
{
    // References to the Main Menu UI
    public GameObject mainMenu;
    public GameObject startBtn;
    public GameObject continueBtn;
    
    // References to 'Start Over' screen
    public GameObject startOver;
    public GameObject startBtn2;
    public GameObject cancelBtn;

    private SaveLoad saveLoad;
    
    void Start()
    {
        MouseLocker.UnlockMouse();
        
        // See if there is save data and use it to enable the correct buttons.
        saveLoad = GetComponent<SaveLoad>();
        saveLoad.LoadGame();
        startBtn.GetComponent<Button>().onClick.AddListener(StartGame);
        if (saveLoad.level == 0) {
            continueBtn.SetActive(false);
            Debug.Log("No save data.");
        }
        else {
            continueBtn.SetActive(true);
            continueBtn.GetComponent<Button>().onClick.AddListener(ContinueGame);
            Debug.Log("Found save data.");
        }
    }

    void StartGame()
    {
        if (saveLoad.level == 0) {
            SaveLoad.SaveGame(1);
            LoadScene(1);
        }
        // Prompt player if they would like to erase game data.
        else {
            
        }
    }

    void ContinueGame()
    {
        LoadScene(saveLoad.level);
    }

    void LoadScene(int scene)
    {
        PersistentData.DidPlayerDie = false;
        SceneManager.LoadScene(scene);
    }
}
*/
