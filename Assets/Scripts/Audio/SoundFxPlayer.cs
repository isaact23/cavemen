using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxPlayer : MonoBehaviour
{
    public AudioSource coinAudioSource;
    public AudioSource jumpscareAudioSource;
    
    public void StartSound(Sound sound)
    {
        if (sound == Sound.CollectCoin) {
            coinAudioSource.Play();
        }
        else if (sound == Sound.Jumpscare) {
            jumpscareAudioSource.Play();
        }
    }

    public void StopSound()
    {
        coinAudioSource.Stop();
        jumpscareAudioSource.Stop();
    }
}
