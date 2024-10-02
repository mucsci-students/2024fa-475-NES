using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;  // Array of different enemy types
    [SerializeField] Transform spawnArea;  // Reference to the area where enemies should spawn (could be a defined region)
    [SerializeField] float spawnAreaWidth = 10f; // Width of spawn area
    [SerializeField] float spawnAreaHeight = 5f; // Height of spawn area
    [SerializeField] float timeDelay = 20f; // Time delay between spawning waves
    [SerializeField] int firstEnemyNum = 0;
    [SerializeField] int secondEnemyNum = 0;

    void Start()
    {
        SpawnEnemies();
        // Start the coroutine to continuously spawn enemies every `timeDelay` seconds
        StartCoroutine(SpawnEnemiesContinuously(timeDelay));
    }
    IEnumerator SpawnEnemiesContinuously(float delay)
    {
        // Infinite loop to continuously spawn enemies
        while (true)
        {
            yield return new WaitForSeconds(delay);

            // Spawn the enemies after waiting
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            // Generate a random number between 3 and 5 (inclusive)
            int enemyCount = Random.Range(firstEnemyNum, secondEnemyNum);

            // Spawn the specified number of enemies of this type
            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 randomPosition = GetRandomSpawnPosition();
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        // Generate random position within the defined spawn area
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float randomY = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        Vector2 spawnPosition = (Vector2)spawnArea.position + new Vector2(randomX, randomY);
        return spawnPosition;
    }
}
