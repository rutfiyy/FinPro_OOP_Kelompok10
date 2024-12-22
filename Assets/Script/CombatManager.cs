using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Wave Configuration")]
    [SerializeField] private List<Wave> waves; // List of all waves
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWaveIndex = -1; // Current wave index (-1 means no wave active)

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
            currentWaveIndex = (currentWaveIndex + 1) % waves.Count;
            Debug.Log($"Starting wave {currentWaveIndex}");

            Wave currentWave = waves[currentWaveIndex];
            currentWave.StartWave();

            Debug.Log($"Wave {currentWave} active for {currentWave.waveDuration} seconds");
            yield return new WaitForSeconds(currentWave.waveDuration);

            currentWave.StopWave();
            Debug.Log($"Wave {currentWaveIndex} stopped");

            yield return new WaitForSeconds(timeBetweenWaves);
            Debug.Log($"Time between waves: {timeBetweenWaves} seconds");
        }
    }
}
