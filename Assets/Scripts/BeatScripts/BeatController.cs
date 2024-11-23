using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeatController : MonoBehaviour
{
    public static BeatController Instance;

    public AudioSource MusicSource;
    public float SongBpm;

    public static float SecPerBeat;
    private float DspSongTime;

    public Transform SpawnPoint;
    public Transform EndPoint;
    public Transform HitPoint;

    public GameObject BeatPrefab;
    public float TrackLengthInSec;

    public float BeatSpawnInterval;
    private float nextBeatTime;

    private float[] spawnDelayList = { 2f, 3f, 4f, 6f, 9f };

    // List to track active beats
    private List<BeatScroller> activeBeats = new List<BeatScroller>();

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
    }

    void Start()
    {
        MusicSource = GetComponent<AudioSource>();

        SecPerBeat = 60f / SongBpm;
        DspSongTime = (float)AudioSettings.dspTime;

        MusicSource.Play();

        BeatSpawnInterval = SecPerBeat;
        nextBeatTime = (float)AudioSettings.dspTime + BeatSpawnInterval;
    }

    private void Update()
    {
        if ((float)AudioSettings.dspTime >= nextBeatTime) // Check if it's time to spawn a new beat
        {
            SpawnBeat();
            nextBeatTime += BeatSpawnInterval;
            BeatSpawnInterval = SecPerBeat / spawnDelayList[Random.Range(0, spawnDelayList.Length)];
        }

        // Handle key press for the closest beat
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandleHit();
        }
    }

    private void SpawnBeat()
    {
        // Creating beat at spawn point
        GameObject newBeat = Instantiate(BeatPrefab, SpawnPoint.position, Quaternion.identity);

        BeatScroller beatScroller = newBeat.GetComponent<BeatScroller>();
        activeBeats.Add(beatScroller); // Add to active beats list

        Debug.Log("The new beat is spawned!");
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

            // Check hit/miss conditions
            float hitDistance = Vector2.Distance(closestBeat.transform.position, HitPoint.position);
            if (hitDistance < 0.2f)
            {
                Debug.Log("Ultra hit!");
                Events.BeatHit(true);
                HitPoint.GetComponent<EndPointController>().OnHit();
            }
            else if (hitDistance < 1f)
            {
                Debug.Log("Successful hit!");
                Events.BeatHit(true);
                HitPoint.GetComponent<EndPointController>().OnHit();
            }
            else
            {
                Debug.Log("Missed hit!");
                Events.BeatHit(false);
                HitPoint.GetComponent<EndPointController>().OnMiss();
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

    public static float GetSongPositionInBeats()
    {
        return ((float)AudioSettings.dspTime - Instance.DspSongTime) / SecPerBeat;
    }
}
