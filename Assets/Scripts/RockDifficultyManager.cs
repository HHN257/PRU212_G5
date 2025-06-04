using UnityEngine;
using System.Collections;

public class RockDifficultyManager : MonoBehaviour
{
    public static RockDifficultyManager instance;
    
    [Header("Difficulty Settings")]
    public float initialFallSpeed = 2f;
    public float maxFallSpeed = 8f;
    public float speedIncreaseRate = 0.1f;
    public float initialSpawnRate = 2f;
    public float minSpawnRate = 0.5f;
    public float spawnRateDecreaseRate = 0.05f;
    
    [Header("Rock Settings")]
    public GameObject[] rockPrefabs;
    public Transform[] spawnPoints;
    
    private float currentFallSpeed;
    private float currentSpawnRate;
    private float gameTime;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        currentFallSpeed = initialFallSpeed;
        currentSpawnRate = initialSpawnRate;
        gameTime = 0f;
        
        // Start spawning rocks
        // InvokeRepeating("SpawnRock", 0f, currentSpawnRate);
        StartCoroutine(SpawnRocksRoutine());
    }
    
    private void Update()
    {
        gameTime += Time.deltaTime;
        
        // Increase difficulty over time
        currentFallSpeed = Mathf.Min(initialFallSpeed + (gameTime * speedIncreaseRate), maxFallSpeed);
        currentSpawnRate = Mathf.Max(initialSpawnRate - (gameTime * spawnRateDecreaseRate), minSpawnRate);
        
        // Update spawn rate - no longer needed with coroutine
        // CancelInvoke("SpawnRock");
        // InvokeRepeating("SpawnRock", 0f, currentSpawnRate);
    }
    
    private System.Collections.IEnumerator SpawnRocksRoutine()
    {
        while (true)
        {
            SpawnRock();
            // Calculate base wait time
            float baseWaitTime = 1f / (1f / currentSpawnRate);
            // Add a small random delay
            float randomDelay = Random.Range(-baseWaitTime * 0.2f, baseWaitTime * 0.2f); // Adjust random range as needed
            float waitTime = baseWaitTime + randomDelay;
            
            yield return new WaitForSeconds(Mathf.Max(0.1f, waitTime)); // Ensure minimum wait time
        }
    }
    
    private void SpawnRock()
    {
        if (rockPrefabs.Length == 0 || spawnPoints.Length == 0) return;
        
        // Randomly select a rock prefab and spawn point
        GameObject rockPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // Instantiate the rock
        GameObject rock = Instantiate(rockPrefab, spawnPoint.position, Quaternion.identity);
        
        // Set the rock's fall speed
        Rock rockScript = rock.GetComponent<Rock>();
        if (rockScript != null)
        {
            rockScript.fallSpeed = currentFallSpeed;
        }
    }
    
    public float GetCurrentFallSpeed()
    {
        return currentFallSpeed;
    }
    
    public float GetCurrentSpawnRate()
    {
        return currentSpawnRate;
    }
} 