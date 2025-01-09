using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller2 : MonoBehaviour
{
    public bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            
        }
        else
        {
            // transform.position = Vector2.Lerp(
            //     SpawnPos,
            //     RemovePos,
            //     (BeatsShownInAdvance - (beatOfThisNote - songPosInBeats)) / BeatsShownInAdvance
            // );  
        }
        
    }
}




















