using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotSpeed;
    public GameObject innerGyro = null; // Optional
    private Vector3 innerRotate = new Vector3(-1, -1, -1);

    void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime * Vector3.right);
        if (innerGyro) {
            innerGyro.transform.Rotate(
                rotSpeed * Time.deltaTime * innerRotate);
        }
    }
}