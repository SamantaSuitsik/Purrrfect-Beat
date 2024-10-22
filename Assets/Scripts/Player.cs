using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;



    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("Idle");
        Events.SetHealth(1);

    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            print(Events.RequestEnemyHealth());
            Events.SetEnemyHealth(Events.RequestEnemyHealth() - 0.05f);
            print(Events.RequestEnemyHealth());
            animator.SetTrigger("Attack");
                
                }
        

    }


}
