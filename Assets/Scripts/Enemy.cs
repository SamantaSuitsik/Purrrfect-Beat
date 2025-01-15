using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Animator animator;

    // Attack timing variables
    [Header("Attack Settings")]
    public float minAttackDelay = 0.8f; // cooldown intervals
    public float maxAttackDelay = 1.5f;
    private float nextAttackTime;

    // Attack execution variables
    private bool isAttacking = false;

    // Dodging variables
    [Header("Dodge Settings")]
    public float minDodgeInterval = 3.0f;
    public float maxDodgeInterval = 4.0f;
    private float nextDodgeTime;
    private bool isDodging = false;
    public float dodgeDuration = 0.5f;
    private float dodgeEndTime;
    private bool isPerformingBeatDodge;
    

    // Health
    public float health = 1.0f;
    public float Damage = 0.01f;
    
    private bool isPlayerDodging;

    private float damagePower;
    private void Awake()
    {
        Events.OnPlayerDodging += PlayerDodging;
        Events.OnBeatHit += OnBeatHit;
        Events.OnMusicEnd += CheckHealthOnMusicEnd;
        Events.OnSetDamagepower += SetDamagepower;
        Events.OnPowerfulAttack += PowerfulAttack;

    }
    
    private void OnDestroy()
    {
        Events.OnPlayerDodging -= PlayerDodging;
        Events.OnBeatHit -= OnBeatHit;
        Events.OnMusicEnd -= CheckHealthOnMusicEnd;
        Events.OnSetDamagepower -= SetDamagepower;
        Events.OnPowerfulAttack -= PowerfulAttack;
    }

    private void PowerfulAttack()
    {
        StartCoroutine(PlayPowerfulAttack());
    }
    
    
    
    IEnumerator PlayPowerfulAttack()
    {
        var attackSound = GameManager.Instance.AttackSound;
        if (attackSound != null)
            attackSound.Play();
        
        isAttacking = true;
        animator.SetTrigger("Lock");

        // Wait for the animation to finish
        yield return new WaitForSeconds(1f);
        
        Events.SetHealth(Events.RequestHealth() - (Damage+0.025f));

        //Events.SetLockBarLetter(lockChars[Random.Range(0, lockChars.Length)]);
        Events.SetLockBarLetter((char)Random.Range('a', 'z'));
    }

    private void SetDamagepower(float obj)
    {
        damagePower = obj;
    }

    private void OnBeatHit(bool isHitOnBeat)
    {
        if (!isHitOnBeat && !isDodging && !isPerformingBeatDodge)
        {
            StartCoroutine(PerformBeatDodge());
            return;
        }
        
        if (isHitOnBeat)
            TakeDamage(damagePower);
    }
    
    private IEnumerator PerformBeatDodge()
    {
        isDodging = true;
        isPerformingBeatDodge = true;
        animator.SetTrigger("Dodge");
        dodgeEndTime = Time.time + dodgeDuration;

        // Wait for the dodge duration
        yield return new WaitForSeconds(dodgeDuration);
        isDodging = false;
        isPerformingBeatDodge = false;
    }

    private void PlayerDodging(bool isPlayerDodging)
    {
        this.isPlayerDodging = isPlayerDodging;

    }

    void Start()
    {
        animator = GetComponent<Animator>();
        nextAttackTime = Time.time + GetRandomAttackDelay();
        nextDodgeTime = Time.time + GetRandomDodgeDelay();
        Events.SetEnemyHealth(health);
    }

    void Update()
    {
        HandleAttacking();
        HandleDodging();
    }

    private void HandleAttacking()
    {
        if (Time.time >= nextAttackTime && !isAttacking && !isDodging)
        {
            StartCoroutine(PerformAttack());
            nextAttackTime = Time.time + GetRandomAttackDelay();
        }

        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword"))
        {
            isAttacking = false;
        }
    }

    private IEnumerator PerformAttack()
    {
        var attackSound = GameManager.Instance.AttackSound;
        if (attackSound != null)
            attackSound.Play();
        
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return null; // Wait one frame
    }

    private void ExecuteAttack()
    {
        // This is called when animation is in the last frame (from Editor)
        if (isPlayerDodging)
            return;
        Events.SetHealth(Events.RequestHealth() - Damage);
    }

    private void HandleDodging()
    {
        // TODO: fix bug where dodgeing happens in the middle of an attack
        if (isDodging)
        {
            
            if (Time.time >= dodgeEndTime) //dodge time has ended
            {
                isDodging = false;
            }
            return;
        }

        if (Time.time >= nextDodgeTime && !isPerformingBeatDodge)
        {
            StartCoroutine(PerformDodge());
            nextDodgeTime = Time.time + GetRandomDodgeDelay();
        }
    }

    private IEnumerator PerformDodge()
    {
        isDodging = true;
        animator.SetTrigger("Dodge");
        dodgeEndTime = Time.time + dodgeDuration;
        var dodgeSound = GameManager.Instance.DodgeSound;
        if (dodgeSound != null)
            dodgeSound.Play();
        
        yield return null; // Wait one frame
    }

    private float GetRandomAttackDelay()
    {
        return UnityEngine.Random.Range(minAttackDelay, maxAttackDelay);
    }

    private float GetRandomDodgeDelay()
    {
        return UnityEngine.Random.Range(minDodgeInterval, maxDodgeInterval);
    }

    public void TakeDamage(float damage)
    {
        if (isDodging)
        {
            Debug.Log("Enemy dodged the attack!");
            return;
        }

        health -= damage;
        animator.SetTrigger("GotDamage");
        Events.SetEnemyHealth(health);

        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            DelayEndGame();
            Events.EndGame(true);

        }
    }


    private IEnumerator DelayEndGame()
    {
        yield return new WaitForSeconds(1.0f);  // Wait for 1 second before ending the game
    }

    private void CheckHealthOnMusicEnd()
    {
        if (Events.RequestEnemyHealth() == Events.RequestHealth())
        {

            Events.EndGame(false);
        }
        
        if (Events.RequestEnemyHealth() < Events.RequestHealth())
        {
            

            animator.SetTrigger("Dead");

            DelayEndGame(); 
            Events.EndGame(true);
        }
    }

}
