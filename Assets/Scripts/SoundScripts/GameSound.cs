using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{

    public AudioClipGroup Group1;
    public AudioClipGroup Group2;
    public AudioClipGroup Group3;


    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Group1.play();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Group2.play();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Group3.play();
        }
    }
}
