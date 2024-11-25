using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    private Transform spawnPoint;
    private Transform endPoint;
    private Transform hitPoint;

    private float moveSpeed;

    void Start()
    {
        spawnPoint = BeatController.Instance.SpawnPoint;
        endPoint = BeatController.Instance.EndPoint;
        hitPoint = BeatController.Instance.HitPoint;

        // Distance between start point (spawn) and endpoint
        float distance = Vector2.Distance(spawnPoint.position, endPoint.position);

        moveSpeed = distance / BeatController.SecPerBeat;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);

        // Check if beat reached endpoint
        if (Vector2.Distance(transform.position, endPoint.position) == 0)
        {
            hitPoint.GetComponent<EndPointController>().OnMiss();

            // Notify BeatController and destroy the beat
            BeatController.Instance.RemoveBeat(this);
            Destroy(gameObject);
        }
    }
}
