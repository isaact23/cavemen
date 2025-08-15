using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public RailAnimationController railAnimController;
    public PlayerMotor playerMotor;

    public GameObject whiteout;
    public GameObject pauseMenu;
    public GameObject continueBtn;
    public GameObject mainMenuBtn;
    public GameObject quitBtn;
    
    private bool paused = false;

    void Start()
    {
        continueBtn.GetComponent<Button>().onClick.AddListener(Unpause);
        mainMenuBtn.GetComponent<Button>().onClick.AddListener(MainMenu);
        quitBtn.GetComponent<Button>().onClick.AddListener(Quit);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Don't allow pausing during enter animation, exit animation or death sequence.
            if (railAnimController.playingEnterAnimation ||
                    railAnimController.playingExitAnimation ||
                    playerMotor.isDead || whiteout.activeSelf) {
                Debug.Log("Can't pause now.");
            }
            else {
                TogglePause();
            }
        }
    }

    private void TogglePause()
    {
        paused = !paused;
        if (paused) {
            Pause();
        }
        else {
            Unpause();
        }
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        //ui.SetActive(false);
        pauseMenu.SetActive(true);
        MouseLocker.UnlockMouse();
        playerMotor.Freeze();
    }

    private void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        //ui.SetActive(true);
        pauseMenu.SetActive(false);
        MouseLocker.LockMouse();
        playerMotor.Unfreeze();
    }
    
    // Go to main menu (scene 0).
    private void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    // Quit the game.
    private void Quit()
    {
        Application.Quit();
    }
}
