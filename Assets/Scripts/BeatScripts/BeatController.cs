using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BeatController : MonoBehaviour
{
    public static BeatController Instance;

    private AudioSource musicSource;
    private float SongBpm;

    public static float SecPerBeat;
    private float DspSongTime;

    public Transform SpawnPoint;
    public Transform EndPoint;
    public Transform HitPoint;
    public GameObject BeatPrefab;
    public RectTransform Canvas;
    private EndPointController endPointController;
    private float playerDamage = 0.2f;

    // // Milestone 2 
    // // Mida väiksemad arvud seda pikem delay
    // private float[] spawnDelayList = { 1f, 2f, 4f, 6f, 8f, 10f };
    // private int spawnDelayListLength;
    //
    // List to track active beats
    private List<BeatScroller> activeBeats = new List<BeatScroller>();
    
    //NEW VER
    private float SongDurationMinutes = 2f;
    public float MinBeatInterval = 1f;
    public float MaxBeatInterval = 3f;
    public List<float> Notes = new List<float>();
    private int nextIndex = 0;
    private float songPosition;
    //the current position of the song (in beats)
    public float songPosInBeats;
    public int beatsShownInAdvance = 3;
    private static float maxHitDistance = 2.5f;
    private bool isBeatPanelLocked;
    private bool isBeatSpawningStarted;

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

        Events.OnSetLockBarLetter += LockTheBeatBar;
        Events.OnUnlockPanel += UnlockPanel;
    }

    private void OnDestroy()
    {
        Events.OnSetLockBarLetter -= LockTheBeatBar;
        Events.OnUnlockPanel -= UnlockPanel;
    }

    private void UnlockPanel()
    {
        isBeatPanelLocked = false;
    }

    private void LockTheBeatBar(char letter)
    {
        isBeatPanelLocked = true;
    }

    void Start()
    {
        // general
        endPointController = HitPoint.GetComponent<EndPointController>();
        playerDamage = GameManager.Instance.PlayerDamage;
        
        // music
        musicSource = GetComponent<AudioSource>();
        SongBpm = GameManager.Instance.SongBpm;
        beatsShownInAdvance = GameManager.Instance.beatsShownInAdvance;
        MaxBeatInterval = GameManager.Instance.MaxBeatInterval;
        MinBeatInterval = GameManager.Instance.MinBeatInterval;
        
        SecPerBeat = 60f / SongBpm;
        DspSongTime = (float)AudioSettings.dspTime;
        
        musicSource.clip = GameManager.Instance.Music;
        musicSource.Play();
        Events.SetSongStart();

        SongDurationMinutes = musicSource.clip.length;
        GenerateBeatPositions();
    }
    
    void GenerateBeatPositions()
    {
        float songDurationBeats = SongBpm * SongDurationMinutes;
        float currentBeat = 1f;

        while (currentBeat <= songDurationBeats)
        {
            Notes.Add(currentBeat);

            // Determine the next interval
            float interval = Random.Range(MinBeatInterval, MaxBeatInterval + 1); // Inclusive of maxBeatInterval

            currentBeat += interval;
        }
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            Debug.Log("Music has stopped. No more beats will be spawned.");
            return;
        }

        // //Milestone 2
        // if ((float)AudioSettings.dspTime >= nextBeatTime) // Check if it's time to spawn a new beat
        // {
        //     SpawnBeat();
        //     nextBeatTime += BeatSpawnInterval;
        //     BeatSpawnInterval = SecPerBeat / spawnDelayList[Random.Range(0, spawnDelayListLength)];
        // }

        songPosition = (float) (AudioSettings.dspTime - DspSongTime);

        songPosInBeats = songPosition / SecPerBeat;
        
        if (nextIndex < Notes.Count && Notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
        {
            SpawnBeat();
        }
        
        // Handle key press for the closest beat
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isBeatPanelLocked)
        {
            HandleHit();
        }
    }

    private void SpawnBeat()
    {
        var beat = Instantiate<GameObject>(BeatPrefab, SpawnPoint.position, Quaternion.identity,Canvas);

        BeatScroller beatScrollerScript = beat.GetComponent<BeatScroller>();
        beatScrollerScript.Initialize(Notes[nextIndex], beatsShownInAdvance);
        
        // Size the note
        RectTransform beatRectTransform = beat.GetComponent<RectTransform>();
        if (beatRectTransform != null)
        {
            beatRectTransform.localScale = new Vector3(132,132,1);
        }
        
        activeBeats.Add(beatScrollerScript); // Add to active beats list
    
        nextIndex++;
    }

    private void HandleHit()
    {
        if (activeBeats.Count > 0)
        {
            // Clean up null or destroyed beats from the list
            activeBeats.RemoveAll(beat => beat == null);

            // Check again after cleaning up
            if (activeBeats.Count == 0) return;

            // Find the closest beat to the hit point
            BeatScroller closestBeat = activeBeats[0];
            float minDistance = Vector2.Distance(closestBeat.transform.position, HitPoint.position);

            foreach (var beat in activeBeats)
            {
                float distance = Vector2.Distance(beat.transform.position, HitPoint.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestBeat = beat;
                }
            }
            
            // float timeDiff = Mathf.Abs(closestBeat.beatOfThisNote - songPosInBeats);
            float timeDiff = Vector2.Distance(closestBeat.transform.position, HitPoint.position);
            if (timeDiff > maxHitDistance)
                return;
            
            if (timeDiff < 0.2f)
            {
                Debug.Log("Ultra hit!");
                Events.SetDamagePower(playerDamage);
                Events.BeatHit(true);
                endPointController.OnUltraHit();
            }
            else if (timeDiff < 0.7f)
            {
                Debug.Log("good hit!");
                Events.SetDamagePower(playerDamage-0.005f);
                Events.BeatHit(true);
                endPointController.OnHit();
            }
            else if (timeDiff < 1f) {
                Events.SetDamagePower(playerDamage-0.01f);
                Events.BeatHit(true);
                endPointController.OnBadHit();
            }
            else
            {
                Debug.Log("Missed hit!");
                Events.BeatHit(false);
                endPointController.OnMiss();
            }

            // Remove and destroy the closest beat
            activeBeats.Remove(closestBeat);
            Destroy(closestBeat.gameObject);
        }
    }

    public void RemoveBeat(BeatScroller beat)
    {
        activeBeats.Remove(beat); // Remove the beat from the active list
    }
}
