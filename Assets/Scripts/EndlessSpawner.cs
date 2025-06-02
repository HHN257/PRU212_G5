using System.Collections;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnRate = 2f;   // Starting spawn interval (in seconds)
    public float minSpawnRate = 0.5f;     // Minimum allowed spawn interval
    public float spawnRateDecrease = 0.1f; // How much to decrease each interval
    public float increaseRateEvery = 10f; // Time in seconds to wait before increasing spawn rate

    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnEnemies());
        StartCoroutine(IncreaseSpawnRate());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnRate);
        }
    }

    IEnumerator IncreaseSpawnRate()
    {
        while (currentSpawnRate > minSpawnRate)
        {
            yield return new WaitForSeconds(increaseRateEvery);
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease);
        }
    }

    void SpawnEnemy()
    {
        Vector2 screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float x = Random.Range(screenMin.x + 0.5f, screenMax.x - 0.5f);
        float y = screenMax.y + 1f;

        Vector3 spawnPosition = new Vector3(x, y, 0f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
