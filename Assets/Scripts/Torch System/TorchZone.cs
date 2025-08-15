using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When a player enters this zone, enable and disable certain lights.
public class TorchZone : MonoBehaviour
{
    public bool initialize = false; // If true, this zone will be activated on scene initialization.
    public GameObject[] torchesToEnable;
    public GameObject[] torchesToDisable;

    void Start()
    {
        if (initialize) {
            Debug.Log("Initializing torches.");
            DisableAllTorches();
            SetTorches();
        }
    }
    
    void OnTriggerEnter(Collider p)
    {
        if (p.CompareTag("Player")) {
            Debug.Log("Player has entered a zone!");
            SetTorches();
        }
    }
    
    // Disable all torches.
    void DisableAllTorches()
    {
        GameObject[] torches = GameObject.FindGameObjectsWithTag("Torch");
        foreach (GameObject torch in torches) {
            torch.GetComponentInChildren<Light>().enabled = false;
        }
    }

    // Enable and disable specified torches.
    void SetTorches()
    {
        foreach (GameObject torch in torchesToEnable) {
            torch.GetComponentInChildren<Light>().enabled = true;
        }
        foreach (GameObject torch in torchesToDisable) {
            torch.GetComponentInChildren<Light>().enabled = false;
        }
    }
}
