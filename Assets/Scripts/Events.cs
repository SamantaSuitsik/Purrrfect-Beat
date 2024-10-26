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
}
