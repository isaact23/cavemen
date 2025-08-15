using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource ambientAudioSource;
    public AudioSource chaseAudioSource;

    public void StartSound(Sound sound)
    {
        if (sound == Sound.AmbientMusic) {
            if (!ambientAudioSource.isPlaying) {
                ambientAudioSource.Play();
            }
            ambientAudioSource.volume = 1f;
        }
        else if (sound == Sound.ChaseMusic) {
            chaseAudioSource.Play();
        }
    }

    public void StopSound(Sound sound)
    {
        if (sound == Sound.AmbientMusic) {
            ambientAudioSource.volume = 0f;
        }
        else if (sound == Sound.ChaseMusic) {
            chaseAudioSource.Stop();
        }
    }

    private void Start()
    {
        StartSound(Sound.AmbientMusic);
    }
}
