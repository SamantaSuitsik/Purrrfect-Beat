using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    public static BeatController Instance;

    public AudioSource MusicSource;
    public float SongBpm;

    public static float SecPerBeat;
    private float DspSongTime;

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
    }

    public static float GetSongPositionInBeats()
    {
        return ((float)AudioSettings.dspTime - Instance.DspSongTime) / SecPerBeat;
    }

}
