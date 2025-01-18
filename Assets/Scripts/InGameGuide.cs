using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class InGameGuide : MonoBehaviour
{
    public GameObject hintPanel;  
    public TextMeshProUGUI hintText;            
    public string[] guideSteps;      
    private int currentStep = 0;     
    private bool isPaused = false;   

    public GameObject beatPanel;     
    public GameObject lockPanel;
    public MusicTimer musicTimer;

    void Start()
    {

        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
            print("hintpanel is NULL");
        }

        if (currentStep == 0)
        {
            print("calling firts hint");
            StartCoroutine(ShowHintWithDelay(0, 0.5f));
        }
        // Waiting for the first beat
        //InvokeRepeating(nameof(CheckForBeats), 0.5f, 0.5f);
    }

    private void Update()
    {
        //If Enter pressed
        if (hintPanel != null &&
            hintPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            CloseHint();
        }
        if (hintPanel == null)
        {
            return;
        }
    }

    /*private void CheckForBeats()
    {
        int currentLevel = GameManager.Instance.CurrentLevel;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            // Check if on the beat panel is at least one beat
            if (beatPanel.transform.childCount > 0 && currentStep == 0 && unlockedLevel == 1 && currentLevel == 1)
            {
                PauseGame();
                ShowHint(0); // First hint
                CancelInvoke(nameof(CheckForBeats)); // Stop checking
            }
        
       
    }*/

    

    public void OnBeatHit() // Beat on hit successful
    {
        if (currentStep == 1)
        {
            StartCoroutine(ShowHintWithDelay(1, 0.5f)); // Second hint
        }
    }

    public void OnEnemyHitPlayer() // Enemy hit player
    {
        if (currentStep == 2)
        {
            StartCoroutine(ShowHintWithDelay(2, 0.5f)); // Third hint
        }
    }

    public void OnLockPanelAppear() // LockPanel is active
    {
        if (currentStep == 3)
        {
            StartCoroutine(ShowHintWithDelay(3, 0.5f)); // Hint 4 with delay
        }
    }

    public void OnLockPanelComplete() // Lockpanel completed
    {
        if (currentStep == 4)
        {
            StartCoroutine(ShowHintWithDelay(4, 1f)); // Hint 5 with delay
        }

    }


    private void ShowHint(int stepIndex)
    {
        int currentLevel = GameManager.Instance.CurrentLevel;
        print("currentlevel = " + currentLevel);
        string opponent = GameManager.Instance.SelectedOpponentPrefab.name;
        print("oponnent name = " + opponent);

        if (currentLevel == 1 && opponent == "BowGuy")
        {
            hintPanel.SetActive(true);
            hintText.text = guideSteps[stepIndex];
            PauseGame();
            currentStep = stepIndex + 1;
            
        }
        else
        {
            Debug.LogWarning("Opponent or opponentButtons not set properly.");
        }
    }

    private IEnumerator ShowHintWithDelay(int stepIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowHint(stepIndex);
    }

    public void CloseHint()
    {
        hintPanel.SetActive(false);
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        if (musicTimer != null)
        {
            musicTimer.PauseTimer();
        }
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        if (musicTimer != null)
        {
            musicTimer.ResumeTimer();
        }
        isPaused = false;
    }
}
