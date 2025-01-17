using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int CurrentLevel { get; set; }
    public int CurrentOpponent { get; set; }
    public GameObject SelectedOpponentPrefab { get; private set; }
    public AudioClip Music { get; private set; }
    public float SongBpm { get; private set; }
    public int  beatsShownInAdvance { get; set; }
    public float MaxBeatInterval { get; set; }
    public float MinBeatInterval { get; set; }
    public float Damage { get; set; }
    public float PlayerDamage { get; set; }
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
        Events.OnEndGame += EndGame;
    }

    private void OnEnable()
    {
        // Events.OnSetHealth += HandlePlayerHealthChange;
    }

    private void OnDisable()
    {
        // Events.OnSetHealth -= HandlePlayerHealthChange;
    }

    private void OnDestroy()
    {
        Events.OnSongStart -= SongStart;
        Events.OnEndGame -= EndGame;
    }

    private void EndGame()
    {
        if (!Events.GameResult)
            return;
        UnlockProgress();
    }

    private void UnlockProgress()
    {
        // Unlock the next opponent in the current level
        string opponentKey = $"Level{CurrentLevel}_UnlockedOpponent";
        int unlockedOpponents = PlayerPrefs.GetInt(opponentKey, 1);

        if (CurrentOpponent == unlockedOpponents && unlockedOpponents < 3)
        {
            PlayerPrefs.SetInt(opponentKey, unlockedOpponents + 1);
            PlayerPrefs.Save();
            Debug.Log($"Unlocked Opponent {unlockedOpponents + 1} in Level {CurrentLevel}");
        }

        // Check if all opponents in the current level are defeated
        if (CurrentOpponent >= 3)
        {
            UnlockNextLevel();
        }
    }

    private void UnlockNextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (CurrentLevel >= unlockedLevel && unlockedLevel < 3)
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
            PlayerPrefs.Save();
            Debug.Log($"Unlocked Level {unlockedLevel + 1}");
        }
    }

    private void SongStart()
    {
        isSongStarted = true;
        SetBarLockTimer();
    }

    public void SelectOpponent(GameObject opponentPrefab, AudioClip opponentMusic, float opponentSongBpm,
        int difficulty, AudioClipGroup dodgeSound, AudioClipGroup oppAttackSound, int currentLevel, int currentOpponent,
        float minBeatInterval, float maxBeatInterval, float damage, float playerDamage)
    {
        SelectedOpponentPrefab = opponentPrefab;
        Music = opponentMusic;
        SongBpm = opponentSongBpm;
        beatsShownInAdvance = difficulty;
        DodgeSound = dodgeSound;
        AttackSound = oppAttackSound;
        CurrentLevel = currentLevel;
        CurrentOpponent = currentOpponent;
        MinBeatInterval = minBeatInterval;
        MaxBeatInterval = maxBeatInterval;
        Damage = damage;
        PlayerDamage = playerDamage;
    }

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.Space)) // TODO ONLY FOR debugging - remove later!!
        {
            UnlockProgress();
        }
    }

    private void SetBarLockTimer()
    {
        barLockTimer = Random.Range(7f, 20f);
    }

    private void lockTheBeatBar()
    {
        // Events.SetLockBarLetter(randomLetter);
        Events.PowerfulAttack();
        
    }

    // private void HandlePlayerHealthChange(float newHealth)
    // {
    //     if (newHealth < Events.RequestHealth()) //  If health decreased
    //     {
    //         missedHits++; //  Increase the number of missed attacks
    //
    //         if (missedHits >= 5) // If the player missed 2 attacks
    //         {
    //             missedHits = 0; 
    //             var randomLetter = (char)Random.Range('a', 'z'); // Random letter generates
    //             Events.SetLockBarLetter(randomLetter); // Lock panel
    //         }
    //     }
    //     else
    //     {
    //         missedHits = 0; 
    //     }
    // }
}