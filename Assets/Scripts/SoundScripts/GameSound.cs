using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{

    public AudioClipGroup Group1;
    public AudioClipGroup Group2;
    public AudioClipGroup failedAttack;
    public AudioClipGroup Group4;

    private void Awake()
    {
        Events.OnBeatHit += OnBeatHit;
        Events.OnSetLockBarLetter += SetLockBarLetter;
    }

    private void OnDestroy()
    {
        Events.OnBeatHit -= OnBeatHit;
        Events.OnSetLockBarLetter += SetLockBarLetter;
    }

    private void SetLockBarLetter(char letter)
    {
        failedAttack.Play();
    }

    private void OnBeatHit(bool isHitOnBeat)
    {
        if (!isHitOnBeat)
        {
            failedAttack.Play();
            return;
        }
        
        // Play successful hit sound
        Group1.Play();
    }

    void Update()
    {
        // TODO: kasuta evente häälte jaoks, mitte hardcodeimist
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Group2.Play();
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Group4.Play();
        }
    }
}
