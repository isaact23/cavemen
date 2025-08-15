using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public GameObject blackout;
    public float blackoutTime = 3f;

    private bool isPlayerDead = false;
    private float timeSinceDeath = 0f;
    
    void Start()
    {
        MouseLocker.LockMouse();
    }

    void Update()
    {
        if (isPlayerDead) {
            timeSinceDeath += Time.deltaTime;
            if (timeSinceDeath >= blackoutTime) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void KillPlayer()
    {
        if (!isPlayerDead) {
            isPlayerDead = true;
            PersistentData.DidPlayerDie = true;
            blackout.SetActive(true);
        }
    }
}
