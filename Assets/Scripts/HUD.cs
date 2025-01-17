using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image HealthBar;
    public Image EnemyHealthBar;

    private void Awake()
    {
        Events.OnSetEnemyHealth += SetEnemyHealth;
        Events.OnSetHealth += SetHealth;
        Events.OnRequestEnemyHealth += RequestEnemyHealth;
        Events.OnRequestHealth += RequestHealth;
    }
    
    private void OnDestroy()
    {
        Events.OnSetEnemyHealth -= SetEnemyHealth;
        Events.OnSetHealth -= SetHealth;
        Events.OnRequestEnemyHealth -= RequestEnemyHealth;
        Events.OnRequestHealth -= RequestHealth;
    }
    private float RequestHealth()
    {
        return HealthBar.fillAmount;
    }

    private float RequestEnemyHealth()
    {
        return EnemyHealthBar.fillAmount;
    }

    private void SetHealth(float obj)
    {
        HealthBar.fillAmount = obj;
    }
    
    private void SetEnemyHealth(float value)
    {
        Debug.Log("setting enemy health to " + value);
        EnemyHealthBar.fillAmount = value;
    }
}
