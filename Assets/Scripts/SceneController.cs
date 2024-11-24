using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

    public void StartNewGame()
    {
        SceneManager.LoadScene("FightingScene1");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void SelectOpponent()
    {
        SceneManager.LoadScene("ChooseOpponent");
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
                EndGameMusicManager.Instance.PlayWinMusic();
            }
            else
            {
                EndGameText.text = "Game over";
                EndGameMusicManager.Instance.PlayLoseMusic();
            }
        }
        else
        {
            Debug.LogError("EndGameText not connected");
        }

    }

}
