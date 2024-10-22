using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private float attackDelay = 2.0f;
    private float nextAttackTime;


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
            nextAttackTime += attackDelay ;
            Events.SetHealth(Events.RequestHealth() - 0.1f);
        }
    }

    private void attack()
    {
        
    }
}
