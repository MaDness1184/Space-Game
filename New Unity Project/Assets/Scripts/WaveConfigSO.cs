using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    public Transform pathPrefab;
    public float moveSpeed = 5f;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab) // Loop through all child transforms in path
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMovespeed()
    {
        return moveSpeed;
    }
}
