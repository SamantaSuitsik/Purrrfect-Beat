using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;

    public Transform spawnPoint;
    public Transform endPoint;
    public Transform hitPoint;
    
    private AudioSource music;
    //the current position of the song (in seconds)
    private float songPosition;

    //the current position of the song (in beats)
    public float songPosInBeats;

    //the duration of a beat
    private float secPerBeat;

    //how much time (in seconds) has passed since the song started
    private float dsptimesong;
    
    //beats per minute of a song
    public float Bpm;

    public float SongDurationMinutes = 2f; //TODO CHANGE DYNAMICALLY

    //keep all the position-in-beats of notes in the song
    public List<float> Notes = new List<float>();
    
    private int nextIndex = 0;
    public GameObject BeatPrefab;
    public int beatsShownInAdvance = 1;
    private int notesTotal;
    public int MinBeatInterval = 1;
    public int MaxBeatInterval = 5;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        
        // notesTotal = (int) Math.Floor(Bpm * 2);
        // Notes = new float[notesTotal];
        // for (int i = 0; i < notesTotal; i += 2)
        // {
        //     Notes[i] = i + 1f;
        // }
        
        music = GetComponent<AudioSource>();
        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = 60f / Bpm;
    
        //record the time when the song starts
        dsptimesong = (float) AudioSettings.dspTime;

        music.clip = GameManager.Instance.Music;
        music.Play();
        
        GenerateBeatPositions();
    }
    
    void GenerateBeatPositions()
    {
        float songDurationBeats = Bpm * SongDurationMinutes;

        float currentBeat = 1f;

        while (currentBeat <= songDurationBeats)
        {
            Notes.Add(currentBeat);

            // Determine the next interval
            int interval = Random.Range(MinBeatInterval, MaxBeatInterval + 1); // Inclusive of maxBeatInterval

            currentBeat += interval;
        }
    }

    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        songPosInBeats = songPosition / secPerBeat;
        
        if (nextIndex < Notes.Count && Notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
        {
            var beat = Instantiate<GameObject>(BeatPrefab, spawnPoint.position, Quaternion.identity);

            BeatScroller beatScrollerScript = beat.GetComponent<BeatScroller>();
            beatScrollerScript.Initialize(Notes[nextIndex], beatsShownInAdvance);
            
            nextIndex++;
        }
    }
}
