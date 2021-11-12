using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<WaveConfigSO> waveConfigs; // Waves the enemy spawner can execute
    public float timeBetweenWaveSpawns = 0f;
    WaveConfigSO currentWave;
    public bool isLooping = false; // Loop the waves of the enemy spawner

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWavesCo());
    }

    public WaveConfigSO GetCurrentWave() // Gets the current wave of the enemy spawner
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWavesCo()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs) // loops through waves
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++) // loops through spawns
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaveSpawns);
            }
        } while (isLooping); // repeat waves if isLooping is true
    }
}
