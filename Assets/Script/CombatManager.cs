using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Wave Configuration")]
    [SerializeField] private List<Wave> waves; // List of all waves
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private GameObject allyPrefab; // Ally unit prefab

    private int currentWaveIndex = -1; // Current wave index (-1 means no wave active)
    private int waveCounter = 0; // Counter to track wave progress for ally spawning

    public List<EnemySpawner> GetActiveSpawners()
    {
        if (currentWaveIndex >= 0 && currentWaveIndex < waves.Count)
        {
            return waves[currentWaveIndex].enemySpawners;
        }
        return new List<EnemySpawner>();
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            // Randomly select the next wave
            currentWaveIndex = Random.Range(0, waves.Count);
            Wave currentWave = waves[currentWaveIndex];
            currentWave.StartWave();

            // Increment the wave counter
            waveCounter++;

            // Spawn an ally every 2 waves
            if (waveCounter % 2 == 0)
            {
                SpawnAlly();
            }

            // Wait for the duration of the current wave
            yield return new WaitForSeconds(currentWave.waveDuration);

            currentWave.StopWave();

            // Wait for the interval between waves
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private void SpawnAlly()
    {
        if (allyPrefab != null)
        {
            Instantiate(allyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Ally Prefab or Spawn Point is not assigned in the Combat Manager.");
        }
    }
}
