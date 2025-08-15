using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public RailAnimationController railAnimController;
    void Start()
    {
        if (PersistentData.DidPlayerDie) {
            railAnimController.FadeIn();
            railAnimController.EndEnterAnimation();
        }
        else {
            railAnimController.StartEnterAnimation();
        }
    }

    public void EndLevel()
    {
        // Save game data.
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(nextScene);
        if (nextScene < 5) {
            SaveLoad.SaveGame(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        // Forget player death.
        PersistentData.DidPlayerDie = false;
        
        // Load next scene.
        if (nextScene >= SceneManager.sceneCountInBuildSettings) {
            //SceneManager.LoadScene(0);
        }
        else {
            SceneManager.LoadScene(nextScene);
        }
    }
}
