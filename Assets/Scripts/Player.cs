using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private float health = 1.0f;
    private bool isDodging = false;
    private float dodgeTime = 1.0f;
    private float dodgetimePlusNormalTime;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("Idle");
        Events.SetHealth(health);

        Events.OnSetHealth += UpdateHealth;
        

    }

    void OnDestroy()
    {
        Events.OnSetHealth -= UpdateHealth;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
// print(Events.RequestEnemyHealth());
            Events.SetEnemyHealth(Events.RequestEnemyHealth() - 0.05f);
// print(Events.RequestEnemyHealth());
            animator.SetTrigger("Attack");

            if (Events.RequestEnemyHealth() <= 0)
            {
                Events.EndGame(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger("Dodge");
            isDodging = true;
            dodgetimePlusNormalTime = dodgeTime + Time.time;
            
            Events.PlayerDodging(true);
        }

        if (isDodging && dodgetimePlusNormalTime < Time.time)
        {
            isDodging = false;
            Events.PlayerDodging(false);
        }

        

    }

    void UpdateHealth(float value)
    {
        // if (isDodging && dodgetimePlusNormalTime >= Time.time)
        // {
        //     print("dodged a hit health:" + health);
        //     // change health back (so did not get hit)
        //     return;
        // }
        
        // Can get hit
        health = value;
            
        if (health <= 0)
        {
            Events.EndGame(false);
        }
    }
}
