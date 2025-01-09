using UnityEngine;
using UnityEngine.SceneManagement;

public class OpponentSelector : MonoBehaviour
{
    // Assign this in the Inspector with the opponent prefab
    public GameObject opponentPrefab;
    public AudioClip opponentMusic;
    public float opponentSongBpm;
    public int difficulty;
    public AudioClipGroup dodgeSound;
    public AudioClipGroup attackSound;

    // Method to be called when this opponent is selected
    public void OnSelectOpponent()
    {
        // Assign the selected opponent to the GameManager
        GameManager.Instance.SelectOpponent(opponentPrefab,opponentMusic,opponentSongBpm, difficulty, dodgeSound, attackSound);
        
        // Load the main game scene
        SceneManager.LoadScene(3); // Replace with your main scene name
    }
}