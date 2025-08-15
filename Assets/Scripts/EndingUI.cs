using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    public GameObject mainMenuBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        MouseLocker.UnlockMouse();
        mainMenuBtn.GetComponent<Button>().onClick.AddListener(OpenMainMenu);
    }

    void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
