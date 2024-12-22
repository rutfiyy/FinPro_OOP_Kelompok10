using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject enemyPrefab; // The enemy to spawn
    [SerializeField] private float spawnInterval = 2f; // Interval between spawns in seconds

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // Instantiate the prefab as a GameObject and get its Enemy component
        GameObject enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError($"The prefab {enemyPrefab.name} does not have an Enemy component attached!");
        }
    }
}
