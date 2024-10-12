using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image HealthBar;
    public Image EnemyHealthBar;

    private void Awake()
    {
        Events.OnSetEnemyHealth += OnSetEnemyHealth;


    }


    void Start()
    {
        
    }

    void Update()
    {

    }
    private void OnSetEnemyHealth(float value)
    {
        EnemyHealthBar.fillAmount = value;
    }
}
