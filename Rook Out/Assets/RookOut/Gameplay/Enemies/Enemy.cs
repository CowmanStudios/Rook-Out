using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Waypoint targetWaypoint { get; set; }

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardTargetWaypoint(speed * Time.deltaTime);
    }

    void MoveTowardTargetWaypoint(float maxDistance)
    {
        // Can't move if there's no target or no speed
        if (targetWaypoint == null || maxDistance < float.Epsilon) return;

        Vector3 startPoint = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, maxDistance);
        
        // Move the rest of the way along the next leg of the path
        if(transform.position == targetWaypoint.position)
        {
            float actualDistance = (transform.position - startPoint).magnitude;
            targetWaypoint = targetWaypoint.Next;
            MoveTowardTargetWaypoint(maxDistance - actualDistance);
        }
    }
}
