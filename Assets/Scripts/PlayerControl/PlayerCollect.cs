using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{
    public AudioManager audioManager;
    public CoinManager coinManager;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Coin")) {
            hit.gameObject.tag = "Untagged";
            GameObject.Destroy(hit.gameObject);
            audioManager.PlayCoinSound();
            coinManager.IncreaseCounter();
        }
    }
}