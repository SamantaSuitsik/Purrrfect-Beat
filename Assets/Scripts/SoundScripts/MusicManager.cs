using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private AudioSource audioSource;

    
    public string[] scenesWithMusic = { "MainMenu", "LevelSelection", "ChooseOpponent" };

    void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (IsSceneWithMusic(scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    
    private bool IsSceneWithMusic(string sceneName)
    {
        foreach (string musicScene in scenesWithMusic)
        {
            if (sceneName == musicScene)
                return true;
        }
        return false;
    }
}
