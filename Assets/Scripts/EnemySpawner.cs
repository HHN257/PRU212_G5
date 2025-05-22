using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        // Get screen bounds in world units
        Vector2 screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Choose a random X within screen width
        float x = Random.Range(screenMin.x + 0.5f, screenMax.x - 0.5f); // 0.5f padding
        float y = screenMax.y + 1f; // Slightly above the screen

        Vector3 spawnPosition = new Vector3(x, y, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
