using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private float health = 1.0f;


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
            print(Events.RequestEnemyHealth());
            Events.SetEnemyHealth(Events.RequestEnemyHealth() - 0.05f);
            print(Events.RequestEnemyHealth());
            animator.SetTrigger("Attack");

            if (Events.RequestEnemyHealth() <= 0)
            {
                Events.EndGame(true);
            }


        }


    }

    void UpdateHealth(float value)
    {
        health = value;
        if (health <= 0)
        {
            Events.EndGame(false);
        }
    }
}
