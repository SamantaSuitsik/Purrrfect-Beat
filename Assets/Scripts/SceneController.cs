using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public TextMeshProUGUI EndGameText;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (EndGameText != null) 
        {
            UpdateEndGameText();
        }
    }

    void OnEnable()
    {
        Events.OnEndGame += SwitchToEndGameScene;
    }

    void OnDisable()
    {
        Events.OnEndGame -= SwitchToEndGameScene;
    }

    void SwitchToEndGameScene()
    {
        SceneManager.LoadScene("EndGame");
    }


    void UpdateEndGameText()
    {
        if (EndGameText != null)
        {
            if (Events.GameResult)
            {
                EndGameText.text = "Level completed";
            }
            else
            {
                EndGameText.text = "Game over";
            }
        }
        else
        {
            Debug.LogError("EndGameText not connected");
        }

    }
}
