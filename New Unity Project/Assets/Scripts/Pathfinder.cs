using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public WaveConfigSO waveConfig; // Reference to SO - path and move speed
    List<Transform> waypoints; // stored waypoints in path
    int waypointIdex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints(); // get the waypoints from the SO
        transform.position = waypoints[waypointIdex].position; // Move to first waypoint
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    void FollowPath() // Move closer to next waypoint every frame
    {
        if (waypointIdex < waypoints.Count) // If not at last waypoint
        {
            Vector3 targetPosition = waypoints[waypointIdex].position; // waypoint position
            float delta = waveConfig.GetMovespeed() * Time.deltaTime; // distance moved each frame
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta); // Movetowards(currentPos, targetPos, maxDistDelta)
            if (transform.position == targetPosition) // increase index of waypoints when waypoint approached
                waypointIdex++;
        }
        else
        {
            Destroy(gameObject); // Destroy this gameObject
        }
    }
}
