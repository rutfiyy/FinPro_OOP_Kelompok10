using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public float waveDuration = 30f; // Duration of the wave in seconds
    public List<EnemySpawner> enemySpawners; // List of spawners for this wave

    public void StartWave()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.enabled = true; // Activate spawner
        }
    }

    public void StopWave()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.enabled = false; // Deactivate spawner
        }
    }
}
