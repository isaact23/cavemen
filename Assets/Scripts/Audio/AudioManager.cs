using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // All implement SoundPlayer
    public SoundFxPlayer soundFxPlayer;
    public MusicPlayer musicPlayer;

    private List<CavemanMotor> cavemenMotors;
    private bool isPlayerBeingChased = false;
    private bool isPlayerAlive = true;

    public void PlayCoinSound()
    {
        if (isPlayerAlive) {
            soundFxPlayer.StartSound(Sound.CollectCoin);
        }
    }

    public void PlayDeathSound()
    {
        isPlayerAlive = false;
        soundFxPlayer.StartSound(Sound.Jumpscare);
        musicPlayer.StopSound(Sound.AmbientMusic);
        musicPlayer.StopSound(Sound.ChaseMusic);
    }

    private void Start()
    {
        musicPlayer.StartSound(Sound.AmbientMusic);
        
        // Get references to all caveman motors in the scene.
        GameObject[] cavemen = GameObject.FindGameObjectsWithTag("Caveman");
        cavemenMotors = new List<CavemanMotor>();
        foreach (GameObject caveman in cavemen) {
            cavemenMotors.Add(caveman.GetComponent<CavemanMotor>());
        }
    }

    private void Update()
    {
        if (isPlayerAlive) {
            // Check for cavemen chasing the player.
            bool isChasing = false;
            foreach (CavemanMotor caveman in cavemenMotors) {
                if (caveman.IsChasing()) {
                    isChasing = true;
                    break;
                }
            }

            // If the state has changed, update chase/ambient music accordingly.
            if (isPlayerBeingChased != isChasing) {
                isPlayerBeingChased = isChasing;
                if (isPlayerBeingChased == true) {
                    musicPlayer.StartSound(Sound.ChaseMusic);
                    musicPlayer.StopSound(Sound.AmbientMusic);
                }
                else {
                    musicPlayer.StartSound(Sound.AmbientMusic);
                    musicPlayer.StopSound(Sound.ChaseMusic);
                }
            }
        }
    }
}
