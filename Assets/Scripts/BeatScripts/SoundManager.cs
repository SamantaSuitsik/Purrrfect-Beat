using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller2 theBS;
    
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                
                theMusic.clip = GameManager.Instance.Music;
                print("music " + theMusic.clip);
                theMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        print("hit");
    }
    
    public void NoteMiss()
    {
        print("miss");
    }
}
