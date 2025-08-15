using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public CoinManager coinManager;
    public RailAnimationController railAnimController;
    public GameObject entranceBlocker;

    private bool didPlayerExit = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coinManager.IsGoalMet() && !didPlayerExit) {
            didPlayerExit = true;
            railAnimController.StartExitAnimation();
            entranceBlocker.SetActive(true);
        }
    }
}
