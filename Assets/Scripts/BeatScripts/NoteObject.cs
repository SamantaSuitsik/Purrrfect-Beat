using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    // todo: Seda ei kasutata kusagil vist? Kustutada ara
    public bool canBePressed;
    public KeyCode attackKey;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {

            if (canBePressed)
            {
                gameObject.SetActive(false);
                SoundManager.instance.NoteHit();
            }

            // float beatDistance = Mathf.Abs(BeatController.GetSongPositionInBeats() % 1);
            //
            // if (beatDistance < 0.2f)
            // {
            //     Debug.Log("Successful hit!");
            //     gameObject.SetActive(false);
            // }
            // else
            // {
            //     Debug.Log("Missed hit");
            // }



        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
            SoundManager.instance.NoteMiss();
        }
    }
}