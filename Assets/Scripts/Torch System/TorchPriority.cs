using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPriority : MonoBehaviour
{
    private GameObject[] torches;
    public GameObject player;

    private void Start()
    {
        torches = GameObject.FindGameObjectsWithTag("Torch");
    }

    private void Update()
    {
        GameObject[] closestTorches = new GameObject[8];
        
    }
}
