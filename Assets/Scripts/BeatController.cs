using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    public float SongBpm;

    public float SecPerBeat;
    public float SongPosition;
    public float SongPositionInBeats;

    public float DspSongTime;

    public AudioSource MusicSource;
    public float FirstBeatOffset;

    void Start()
    {
        
        MusicSource = GetComponent<AudioSource>();

        SecPerBeat = 60f / SongBpm;

        DspSongTime = (float)AudioSettings.dspTime;

        MusicSource.Play();
    }


    void Update()
    {
        SongPosition = (float)(AudioSettings.dspTime - DspSongTime - FirstBeatOffset);
        SongPositionInBeats = SongPosition / SecPerBeat;


    }
}
