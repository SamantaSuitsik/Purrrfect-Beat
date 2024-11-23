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
    
    [Header("Dodge Cooldown Settings")]
    public float dodgeCooldown = 5.0f;
    private float nextDodgeAvailableTime = 0f;
    private bool isCooldown = false;


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
        handleAttack();
        handleDodge();
        HandleCooldown();
    }

    private void handleDodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isDodging && !isCooldown)
        {
            animator.SetTrigger("Dodge");
            isDodging = true;
            dodgetimePlusNormalTime = dodgeTime + Time.time;
            
            Events.PlayerDodging(true);
            isCooldown = true;
            nextDodgeAvailableTime = Time.time + dodgeCooldown;
        }

        if (isDodging && dodgetimePlusNormalTime < Time.time)
        {
            isDodging = false;
            Events.PlayerDodging(false);
        }
    }

    void handleAttack()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger("Attack");
            Events.TriggerPlayerAttack();
        }
    }
    
    private void HandleCooldown()
    {
        if (isCooldown && Time.time >= nextDodgeAvailableTime)
        {
            isCooldown = false;
            Debug.Log("Dodge is ready!");
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
