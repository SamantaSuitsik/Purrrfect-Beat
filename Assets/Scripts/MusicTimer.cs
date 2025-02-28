using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicTimer : MonoBehaviour
{
    public AudioSource musicSource;
    public TextMeshProUGUI timerText;         

    private float musicDuration;
    private float pausedTime = 0f; 
    private bool isPaused = false;
    private bool isPulsating = false; // if music time is 10 sec
    public bool hasTriggeredMusicEnd = false;


    void Start()
    {
        if (GameManager.Instance.Music != null)
        {
            musicDuration = GameManager.Instance.Music.length;
            print("Music duration = " + musicDuration);
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is missing!");
        }

    }


    private void OnMusicEndHandler()
    {
        Events.TriggerMusicEnd();
    }
    

    void Update()
    {
        if (!isPaused && musicSource != null && musicSource.isPlaying)
        {

            float remainingTime = Mathf.Max(0, musicDuration - musicSource.time);

            // Formating time
            string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
            string seconds = Mathf.Floor(remainingTime % 60).ToString("00");
            timerText.text = $"{minutes}:{seconds}";

            if (remainingTime <= 10)
            {
                timerText.color = Color.red;
                if (!isPulsating)
                {
                    isPulsating = true;
                    StartCoroutine(PulseTimer());
                }
            }
        }
        else if (musicSource != null && !musicSource.isPlaying && !hasTriggeredMusicEnd && !isPaused)
        {
            
            timerText.text = "00:00";
            StopAllCoroutines();
            timerText.transform.localScale = Vector3.one;
            isPulsating = false;

            
            hasTriggeredMusicEnd = true;
            OnMusicEndHandler();
          
        }
    }
    public void PauseTimer()
    {
        if (!isPaused)
        {
            isPaused = true;
            pausedTime += musicSource.time;
            musicSource.Pause(); 
        }
    }

    public void ResumeTimer()
    {
        if (isPaused)
        {
            isPaused = false;
            musicSource.time = pausedTime;
            musicSource.Play();
        }
    }


    private IEnumerator PulseTimer()
    {
        while (isPulsating)
        {
           
            for (float t = 0; t <= 1; t += Time.unscaledDeltaTime * 2)
            {
                timerText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.2f, t);
                yield return null;
            }


            for (float t = 0; t <= 1; t += Time.unscaledDeltaTime * 2)
            {
                timerText.transform.localScale = Vector3.Lerp(Vector3.one * 1.2f, Vector3.one, t);
                yield return null;
            }
        }
    }
}
