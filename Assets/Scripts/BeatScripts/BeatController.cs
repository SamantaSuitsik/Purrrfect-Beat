using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

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
        if ((float)AudioSettings.dspTime >= nextBeatTime) //Check if it's time to spawn new beat
        {
            SpawnBeat();
            nextBeatTime += BeatSpawnInterval;
        }
    }

    private void SpawnBeat()
    {
        // Creating beat on spawnpoint
        GameObject newBeat = Instantiate(BeatPrefab, SpawnPoint.position, Quaternion.identity);

        
        BeatScroller beatScroller = newBeat.GetComponent<BeatScroller>();

        Debug.Log("The new beat is spawned!");
    }

    public static float GetSongPositionInBeats()
    {
        return ((float)AudioSettings.dspTime - Instance.DspSongTime) / SecPerBeat;
    }

}
