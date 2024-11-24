using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Events
{
    public static event Action<float> OnSetHealth;
    public static void SetHealth(float value) => OnSetHealth?.Invoke(value);

    public static event Func<float> OnRequestHealth;
    public static float RequestHealth() => OnRequestHealth?.Invoke() ?? 0;
    
    public static event Action<float> OnSetEnemyHealth;
    public static void SetEnemyHealth(float value) => OnSetEnemyHealth?.Invoke(value);

    public static event Func<float> OnRequestEnemyHealth;
    public static float RequestEnemyHealth() => OnRequestEnemyHealth?.Invoke() ?? 0;

    public static bool GameResult;

    public static event Action OnEndGame;
    public static void EndGame(bool gameResult)
    {
        GameResult = gameResult;
        OnEndGame?.Invoke();
    }
    public static event Action<bool> OnPlayerDodging; // New event for dodging state
    public static void PlayerDodging(bool isDodging) => OnPlayerDodging?.Invoke(isDodging);

    public static Action OnPlayerAttack;

    public static void TriggerPlayerAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    public static event Action<bool> OnBeatHit;

    public static void BeatHit(bool isHitOnBeat)
    {
        OnBeatHit?.Invoke(isHitOnBeat);
    }

    public static event Action OnMusicEnd; // Music end event
    public static void TriggerMusicEnd() => OnMusicEnd?.Invoke();

    public static void CheckHealthOnMusicEnd()
    {
        if (RequestEnemyHealth() > RequestHealth()) 
        {
            EndGame(false); 
        }
        else if (RequestEnemyHealth() < RequestHealth()) 
        {
            EndGame(true);
        }
        else
        {
            EndGame(false); 
        }
    }

}
