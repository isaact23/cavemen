using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RailAnimationController : MonoBehaviour
{
    public GameObject player;
    public GameObject minecartCamera;
    private Animator cartAnimator;
    public Animator uiAnimator;
    public SceneTransition sceneTransition;

    [HideInInspector] public bool playingEnterAnimation = false;
    [HideInInspector] public bool playingExitAnimation = false;
    
    private void Awake()
    {
        cartAnimator = GetComponent<Animator>();
    }
    
    // Start the minecart opening scene.
    public void StartEnterAnimation()
    {
        playingEnterAnimation = true;
        player.SetActive(false);
        minecartCamera.SetActive(true);
        cartAnimator.SetInteger("SceneNo", SceneManager.GetActiveScene().buildIndex);
        cartAnimator.SetBool("Exit", false);
        cartAnimator.enabled = true;
    }

    // Transition control to the player.
    public void EndEnterAnimation()
    {
        playingEnterAnimation = false;
        player.SetActive(true);
        minecartCamera.SetActive(false);
        cartAnimator.enabled = false;
    }

    public void StartExitAnimation()
    {
        playingExitAnimation = true;
        FadeOut();
        uiAnimator.enabled = true;
        Invoke("EndLevel", 4.5f);
    }

    // Remove whiteout.
    public void FadeIn()
    {
        uiAnimator.SetBool("Fade In", true);
    }

    // Create whiteout.
    public void FadeOut()
    {
        uiAnimator.SetBool("Fade In", false);
    }

    public void EndLevel()
    {
        sceneTransition.EndLevel();
    }
}
