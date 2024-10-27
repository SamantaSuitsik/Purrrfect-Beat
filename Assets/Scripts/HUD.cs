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
    private bool isPlayerDodging = false; 

    private void Awake()
    {
        Events.OnSetEnemyHealth += SetEnemyHealth;
        Events.OnSetHealth += SetHealth;
        Events.OnRequestEnemyHealth += RequestEnemyHealth;
        Events.OnRequestHealth += RequestHealth;
        Events.OnPlayerDodging += PlayerDodging;


    }

    private void PlayerDodging(bool isDodging)
    {
        isPlayerDodging = isDodging;
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
        if(Events.RequestHealth() > obj)
        {

        }
        
        HealthBar.fillAmount = obj;
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
    private void SetEnemyHealth(float value)
    {
        EnemyHealthBar.fillAmount = value;
    }
}
