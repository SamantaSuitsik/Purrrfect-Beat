using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    private Transform spawnPoint;
    private Transform endPoint;
    private Transform hitPoint;

    private float moveSpeed;
    //private bool hasTriggeredEndPoint = false;

    void Start()
    {
        spawnPoint = BeatController.Instance.SpawnPoint;
        endPoint = BeatController.Instance.EndPoint;
        hitPoint = BeatController.Instance.HitPoint;

        // Distance between startpoint(spawn) and endpoint
        float distance = Vector2.Distance((spawnPoint.position), endPoint.position);

        moveSpeed = distance / BeatController.SecPerBeat;
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);


        // check if beat reached endpoint
        if (Vector2.Distance(transform.position, endPoint.position) == 0)
        {

            hitPoint.GetComponent<EndPointController>().OnMiss();
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (Vector2.Distance(transform.position, hitPoint.position) < 0.2f)
            {
                Debug.Log("Ultra hit!");

                hitPoint.GetComponent<EndPointController>().OnHit();
            }
            else if (Vector2.Distance(transform.position, hitPoint.position) < 1f)
            {
                Debug.Log("Successful hit!");

                hitPoint.GetComponent<EndPointController>().OnHit();
            }
            else
            {
                Debug.Log("Missed hit!");

                hitPoint.GetComponent<EndPointController>().OnMiss();
            }
            Destroy(gameObject);
        }


        
    }
}