using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private float attackDelay = 2.0f;
    private float nextAttackTime;

    private void Awake()
    {
        Events.OnSetEnemyHealth += OnSetEnemyHealth;
    }

    private void OnSetEnemyHealth(float obj)
    {
        
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        nextAttackTime = Time.time;
        Events.SetEnemyHealth(1);
    }


    void Update()
    {

        if (nextAttackTime < Time.time) {
            print("ISe oled lOll");
            animator.SetTrigger("Attack");
            nextAttackTime += attackDelay ;
        }
    }

    private void attack()
    {
        
    }
}
