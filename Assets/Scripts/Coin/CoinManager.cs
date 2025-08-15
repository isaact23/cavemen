using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text coinCountText;
    public int coinGoal;

    private int coinCounter = 0;

    public void IncreaseCounter()
    {
        coinCounter++;
        UpdateText();
    }

    public bool IsGoalMet()
    {
        return coinCounter >= coinGoal;
    }
    
    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (coinCountText) {
            coinCountText.text = coinCounter + "/" + coinGoal + " coins collected";
        }
    }
}
