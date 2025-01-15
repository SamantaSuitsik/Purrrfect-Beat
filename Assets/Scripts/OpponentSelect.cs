using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentSelect : MonoBehaviour
{
    public Button[] opponentButtons;
    public int currentLevel;

    void Start()
    {
        string key = $"Level{currentLevel}_UnlockedOpponent";
        int unlockedOpponents = PlayerPrefs.GetInt(key, 1);

        for (int i = 0; i < opponentButtons.Length; i++)
        {
            if (i < unlockedOpponents)
            {
                opponentButtons[i].interactable = true;
            }
            else
            {
                opponentButtons[i].interactable = false;
            }
        }
    }
}
