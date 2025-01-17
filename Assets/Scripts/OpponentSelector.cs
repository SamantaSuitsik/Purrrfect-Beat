using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpponentSelector : MonoBehaviour
{
    public int CurrentLevel;
    public int CurrentOpponent;
    public GameObject opponentPrefab;
    public AudioClip opponentMusic;
    public float opponentSongBpm;
    public int beatsShownInAdvance;
    public float MaxBeatInterval;
    public float MinBeatInterval;
    public float Damage;
    public float playerDamage;
    public AudioClipGroup dodgeSound;
    public AudioClipGroup attackSound;
    public String Scene;

    // Method to be called when this opponent is selected
    public void OnSelectOpponent()
    {
        // Assign the selected opponent to the GameManager
        GameManager.Instance.SelectOpponent(opponentPrefab,opponentMusic,opponentSongBpm, beatsShownInAdvance, dodgeSound, attackSound, CurrentLevel, CurrentOpponent, MinBeatInterval, MaxBeatInterval, Damage, playerDamage);
        
        // Load the main game scene
        SceneManager.LoadScene(Scene); // Replace with your main scene name
    }
}