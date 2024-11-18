using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private float attackDelay = 2.0f;
    private float nextAttackTime;
    private float actualAttackDelay = 0.2f;
    private float actualAttackTime = 0;
    private bool isAttacking = false;
    private bool isPlayerDodging = false;

    private void Awake()
    {
        Events.OnPlayerDodging += PlayerDodging;
    }

    private void OnDestroy()
    {
        Events.OnPlayerDodging -= PlayerDodging;
    }


    private void PlayerDodging(bool isDodging)
    {
        isPlayerDodging = isDodging;
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
            animator.SetTrigger("Attack");
            nextAttackTime += attackDelay;

            actualAttackTime = Time.time + actualAttackDelay;
            isAttacking = true;

        }
        if (isAttacking && Time.time >= actualAttackTime)
        {
            if (!isPlayerDodging)
            {
                //Give time to react
                //print("actual attack ");
                Events.SetHealth(Events.RequestHealth() - 0.1f);
            }
            isAttacking = false;

        }
     
    }

}
