using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchDisabler : MonoBehaviour
{
    private Transform playerTransform;
    private Light pointLight;
    private float disableDistance = 40f;

    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTransform) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (playerTransform) {
            if (Vector3.Distance(playerTransform.position, pointLight.transform.position) > disableDistance) {
                pointLight.enabled = false;
            }
            else {
                pointLight.enabled = true;
            }
        }
    }
}
