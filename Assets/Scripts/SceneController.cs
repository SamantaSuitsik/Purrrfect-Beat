using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public TextMeshProUGUI EndGameText;
    public Transform EnemySpawnPoint;
    public Button NextLevelButton;

    private bool isPaused = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (NextLevelButton != null)
        {
            NextLevelButton.gameObject.SetActive(false); // by default Next level button is not active
        } 

        if (EndGameText != null)
        {
            UpdateEndGameText();
        }

        if (EnemySpawnPoint == null) return;
        if (GameManager.Instance.SelectedOpponentPrefab != null && EnemySpawnPoint.position !=null)
        {
            Instantiate(GameManager.Instance.SelectedOpponentPrefab, EnemySpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No opponent selected!");
        }
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive); 
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Pause"); 
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

    public void SelectOpponentStreet()
    {
        SceneManager.LoadScene("ChooseOpponentStreet");
    }

    public void SelectOpponentShelter()
    {
        SceneManager.LoadScene("ChooseOpponentShelter");
    }

    public void SelectOpponentHome()
    {
        SceneManager.LoadScene("ChooseOpponentHome");
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
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
                EndGameText.text = "Great! You won!";
                EndGameMusicManager.Instance.PlayWinMusic();

                if (NextLevelButton != null)
                {
                    NextLevelButton.gameObject.SetActive(true);
                }
            }
            else
            {
                if (NextLevelButton != null)
                {
                    NextLevelButton.gameObject.SetActive(false);
                }

                EndGameText.text = "You lose!";
                EndGameMusicManager.Instance.PlayLoseMusic();
            }
        }
        else
        {
            Debug.LogError("EndGameText not connected");
        }

    }

    public void resetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progress Reset");
        
        SelectLevel();
    }

}
