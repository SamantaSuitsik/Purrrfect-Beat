using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject SelectedOpponentPrefab { get; private set; }
    public AudioClip Music { get; private set; }
    public float SongBpm { get; private set; }
    public int DifficultyMultiplier { get; set; }
    public AudioClipGroup DodgeSound { get; set; }
    public AudioClipGroup AttackSound { get; set; }
    private bool isSongStarted;
    private float barLockTimer = 10000f;

    private int missedHits = 0;

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

        Events.OnSongStart += SongStart;

    }

    private void OnEnable()
    {
        Events.OnSetHealth += HandlePlayerHealthChange;
    }

    private void OnDisable()
    {
        Events.OnSetHealth -= HandlePlayerHealthChange;
    }

    private void OnDestroy()
    {
        Events.OnSongStart -= SongStart;
    }

    private void SongStart()
    {
         isSongStarted = true;
        //SetBarLockTimer();
    }

    public void SelectOpponent(GameObject opponentPrefab, AudioClip opponentMusic, float opponentSongBpm,
        int difficulty, AudioClipGroup dodgeSound, AudioClipGroup oppAttackSound)
    {
        SelectedOpponentPrefab = opponentPrefab;
        Music = opponentMusic;
        SongBpm = opponentSongBpm;
        DifficultyMultiplier = difficulty;
        DodgeSound = dodgeSound;
        AttackSound = oppAttackSound;
    }

    /*private void Update()
    {
        if (!isSongStarted)
            return;

        if (barLockTimer <= 0)
        {
            lockTheBeatBar();
            SetBarLockTimer();
        }

        else
        {
            barLockTimer -= Time.deltaTime;
        }
    }

    private void SetBarLockTimer()
    {
        barLockTimer = Random.Range(2f, 8f);
    }

    private void lockTheBeatBar()
    {
        var randomLetter = (char)Random.Range('A', 'Z');
        print(randomLetter);
        Events.SetLockBarLetter(randomLetter);
        
    }*/

    private void HandlePlayerHealthChange(float newHealth)
    {
        if (newHealth < Events.RequestHealth()) //  If health decreased
        {
            missedHits++; //  Increase the number of missed attacks

            if (missedHits >= 5) // If the player missed 2 attacks
            {
                missedHits = 0; 
                var randomLetter = (char)Random.Range('A', 'Z'); // Random letter generates
                Events.SetLockBarLetter(randomLetter); // Lock panel
            }
        }
        else
        {
            missedHits = 0; 
        }
    }
}