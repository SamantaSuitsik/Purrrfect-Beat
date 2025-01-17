using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Start()
    {
        // Waiting for the first beat
        InvokeRepeating(nameof(CheckForBeats), 0.5f, 0.5f);
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

    private void CheckForBeats()
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
        
       
    }

    public void OnBeatHit() // Beat on hit successful
    {
        if (currentStep == 1)
        {
            ShowHint(1); // Second hint
        }
    }

    public void OnEnemyHitPlayer() // Enemy hit player
    {
        if (currentStep == 2)
        {
            ShowHint(2); // Third hint
        }
    }

    public void OnLockPanelAppear() // LockPanel is active
    {
        if (currentStep == 3)
        {
            StartCoroutine(ShowHintWithDelay(3, 1f)); // Hint 4 with delay
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
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (unlockedLevel == 1 && currentLevel == 1)
        {
            hintPanel.SetActive(true);
            hintText.text = guideSteps[stepIndex];
            PauseGame();
            currentStep = stepIndex + 1;
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
