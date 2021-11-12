using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    public List<GameObject> enemyPrefabs; // Enemys in the this wave
    public Transform pathPrefab; // Prefab of path object with waypoints
    public float moveSpeed = 5f; // movespeed of enemy

    public float timeBetweenEnemySpawns = 1f;
    public float spawnTimeVariance = 0f;
    public float minSpawnTime = 0.2f;

    public int GetEnemyCount() // returns total number of enemies in wave
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index) // returns prefab of enemy at index parameter
    {
        return enemyPrefabs[index];
    }

    public Transform GetStartingWaypoint() // returns the starting waypoint of enemy path
    {
        return pathPrefab.GetChild(0);
    }

    public float GetMovespeed() // returns movespeed
    {
        return moveSpeed;
    }

    public float GetRandomSpawnTime() // returns random spawn used time between enemy spawns
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                                        timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }

    public List<Transform> GetWaypoints() // returns a list of transforms with the waypoints for the enemy to follow
    {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform childTransform in pathPrefab) // Loop through all child transforms in path
        {
            waypoints.Add(childTransform); 
        }
        return waypoints;
    }
}
