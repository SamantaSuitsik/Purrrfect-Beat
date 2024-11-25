using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject SelectedOpponentPrefab { get; private set; }
    public AudioClip Music { get; private set; }
    public float SongBpm { get; private set; }
    public int DifficultyMultiplier { get; set; }
    public AudioClipGroup DodgeSound { get; set; }
    public AudioClipGroup AttackSound { get; set; }

    private void Awake()
    {
        // If an instance of GameManager doesn't exist, set it to this
        // and make it persist across scenes.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists and it's not this, destroy this to enforce the singleton pattern.
            Destroy(gameObject);
        }
    }

    public void SelectOpponent(GameObject opponentPrefab, AudioClip opponentMusic, float opponentSongBpm,
        int difficulty, AudioClipGroup dodgeSound, AudioClipGroup oppAttackSound)
    {
        print("opponent selected");
        SelectedOpponentPrefab = opponentPrefab;
        Music = opponentMusic;
        SongBpm = opponentSongBpm;
        DifficultyMultiplier = difficulty;
        DodgeSound = dodgeSound;
        AttackSound = oppAttackSound;
    }
    
}