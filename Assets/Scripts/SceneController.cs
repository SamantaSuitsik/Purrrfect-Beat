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
    public GameObject PauseMenuUI;
    
    public MusicTimer musicTimer;

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

        if (PauseMenuUI != null)
        {
            PauseMenuUI.SetActive(false); 
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (musicTimer != null)
        {
            musicTimer.PauseTimer();
        }


        if (PauseMenuUI != null)
        {
            PauseMenuUI.SetActive(true); 
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        if (musicTimer != null)
        {
            musicTimer.ResumeTimer(); 
        }

        

        if (PauseMenuUI != null)
        {
            PauseMenuUI.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("FightingScene1");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
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

    public void LoadNextLevel()
    {
        
        int currentLevel = GameManager.Instance.CurrentLevel;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        
        if (unlockedLevel == 1)
        {
            
            string nextScene = $"Level{currentLevel + 1}";
            if (nextScene == "Level2" || nextScene == "Level3")
            {
                nextScene = "ChooseOpponentStreet";
            }

            
            SceneManager.LoadScene(nextScene);
            Debug.Log($"Loading {nextScene}");
        }

        if (unlockedLevel == 2)
        {

            string nextScene = $"Level{currentLevel + 1}";
            if (nextScene == "Level2" || nextScene == "Level3")
            {
                nextScene = "ChooseOpponentShelter";
            }


            SceneManager.LoadScene(nextScene);
            Debug.Log($"Loading {nextScene}");
        }

        if (unlockedLevel == 3)
        {

            string nextScene = $"Level{currentLevel + 1}";
            if (nextScene == "Level2" || nextScene == "Level3")
            {
                nextScene = "ChooseOpponentStreet";
            }


            SceneManager.LoadScene(nextScene);
            Debug.Log($"Loading {nextScene}");
        }



        else
        {
            Debug.Log("Next level is locked or not yet available.");
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
