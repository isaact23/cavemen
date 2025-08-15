using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to force the player to look at the assailant.
public class PlayerForceLook : MonoBehaviour
{
    public float lookSpeed;
    public float yOffset;
    
    private bool looking = false;
    private GameObject target;
    private PlayerMotor playerMotor;

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    public void StartLooking(GameObject targetObject)
    {
        looking = true;
        target = targetObject;
        playerMotor.Freeze();
        playerMotor.isDead = true;
    }

    private void Update()
    {
        if (looking) {
            Vector3 direction = target.transform.position - transform.position + new Vector3(0, yOffset, 0);
            Vector3 lookVector = Vector3.RotateTowards(transform.forward, direction, lookSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(lookVector);
        }
    }
}
