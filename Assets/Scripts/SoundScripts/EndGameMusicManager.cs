using System.Collections;
using UnityEngine;

public class EndGameMusicManager : MonoBehaviour
{
    public static EndGameMusicManager Instance;

    [Header("Audio Groups")]
    public AudioClipGroup winAudioGroup; 
    public AudioClipGroup loseAudioGroup; 

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

   
    public void PlayWinMusic()
    {
        StartCoroutine(PlayGroupMusic(winAudioGroup));
    }

    
    public void PlayLoseMusic()
    {
        StartCoroutine(PlayGroupMusic(loseAudioGroup));
    }

    
    private IEnumerator PlayGroupMusic(AudioClipGroup clipGroup)
    {
        foreach (AudioClip clip in clipGroup.Clips)
        {
            if (clip == null) continue;

            audioSource.clip = clip;
            audioSource.Play();

            
            yield return new WaitForSeconds(clip.length);
        }
    }
}
