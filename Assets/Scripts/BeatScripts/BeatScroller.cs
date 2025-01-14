using System;
using System.Net;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    private Transform SpawnPoint;
    private Transform EndPoint;
    private Transform HitPoint;
    public float beatOfThisNote { get; set; }
    private float beatsShownInAdvance;
    private float songPosInBeats;
    private EndPointController endPointCotroller;
    private float totalTravelTime;
    private float speedPhase1; 
    private float leftoverBeats;
    public void Initialize(float beat, float beatsShownInAdvance)
    {
        beatOfThisNote = beat;
        this.beatsShownInAdvance = beatsShownInAdvance;
    }

    void Start()
    {
        // music related
        //Milestone 2 version
        SpawnPoint = BeatController.Instance.SpawnPoint;
        EndPoint = BeatController.Instance.EndPoint;
        HitPoint = BeatController.Instance.HitPoint;
        
        float distSpawnToHit = Vector2.Distance(SpawnPoint.position, HitPoint.position);
        float distHitToEnd = Vector2.Distance(HitPoint.position, EndPoint.position);
        
        // Distance per beat during phase 1
        speedPhase1 = distSpawnToHit / beatsShownInAdvance;
        // How many beats to travel from Hit to End at the same speed
        leftoverBeats = distHitToEnd / speedPhase1;
        
        // // Distance between start point (spawn) and endpoint
        // float distance = Vector2.Distance(SpawnPoint.position, EndPoint.position);
        //
        // moveSpeed = distance / BeatController.SecPerBeat;

        // SpawnPoint = SongManager.Instance.spawnPoint;
        // EndPoint = SongManager.Instance.EndPoint;
        // HitPoint = SongManager.Instance.hitPoint;
        // endPointCotroller = HitPoint.GetComponentInChildren<EndPointController>();

    }

    void Update()
    {
        // Milestone 2 version
        // transform.position = Vector2.MoveTowards(transform.position, EndPoint.position, moveSpeed * Time.deltaTime);
        
        // // New version
        // songPosInBeats = BeatController.Instance.songPosInBeats;
        //
        // transform.position = Vector2.Lerp(
        //     SpawnPoint.position,
        //     EndPoint.position,
        //     (beatsShownInAdvance - (beatOfThisNote - songPosInBeats)) / beatsShownInAdvance
        // );
        
        // // In Update of BeatScroller:
        // float currentBeat = BeatController.Instance.songPosInBeats;
        // if (currentBeat < beatOfThisNote) {
        //     // Phase 1: from spawn to hit
        //     float t = (currentBeat - (beatOfThisNote - beatsShownInAdvance)) / beatsShownInAdvance;
        //     transform.position = Vector2.Lerp(SpawnPoint.position, HitPoint.position, t);
        // } else {
        //     // Phase 2: from hit to end
        //     // Add however many extra beats you want for traveling from hit to end
        //     float t2 = (currentBeat - beatOfThisNote);
        //     transform.position = Vector2.Lerp(HitPoint.position, EndPoint.position, t2);
        // }
        float currentBeat = BeatController.Instance.songPosInBeats;

        if (currentBeat < beatOfThisNote) {
            // Phase 1
            float t = (currentBeat - (beatOfThisNote - beatsShownInAdvance)) / beatsShownInAdvance;
            transform.position = Vector2.Lerp(SpawnPoint.position, HitPoint.position, t);
        } else {
            // Phase 2 (same speed as phase 1)
            float t2 = (currentBeat - beatOfThisNote) / leftoverBeats;
            transform.position = Vector2.Lerp(HitPoint.position, EndPoint.position, t2);
        }
        
        // Milestone 2 and new ver
        // Check if reached end
        if (Vector2.Distance(transform.position, EndPoint.position) < 0.05f)
        {
            HitPoint.GetComponent<EndPointController>().OnMiss();
        
            // Notify BeatController and destroy the beat
            BeatController.Instance.RemoveBeat(this);
            Destroy(gameObject);
        }
    }
}
