using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;  // Array of different enemy types
    [SerializeField] Transform spawnArea;  // Reference to the area where enemies should spawn (could be a defined region)
    [SerializeField] float spawnAreaWidth = 10f; // Width of spawn area
    [SerializeField] float spawnAreaHeight = 5f; // Height of spawn area
    [SerializeField] float timeDelay = 8f; // Time delay between spawning waves
    [SerializeField] int firstEnemyNum = 0;
    [SerializeField] int secondEnemyNum = 0;
    [SerializeField] float enemyHealth = 100f;
    [SerializeField] int enemyDamage = 10;
    [SerializeField] float enemySpeed = 1;


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
            // Generate a random number between firstEnemyNum and secondEnemyNum
            int enemyCount = Random.Range(firstEnemyNum, secondEnemyNum);

            // Spawn the specified number of enemies of this type
            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 randomPosition = GetRandomSpawnPosition();
                GameObject spawnedEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

                // Now modify the stats of the spawned enemy
                EnemyStat enemyStat = spawnedEnemy.GetComponent<EnemyStat>();
                if (enemyStat != null)
                {
                    enemyStat.SetEnemyHealth(enemyHealth);
                    enemyStat.SetEnemyDamage(enemyDamage);
                }

                // Handle ground and air enemy movement speeds
                GroundEnemyMovements groundEnemyMovement = spawnedEnemy.GetComponent<GroundEnemyMovements>();
                AirEnemyMovements airEnemyMovements = spawnedEnemy.GetComponent<AirEnemyMovements>();

                if (groundEnemyMovement != null)
                {
                    groundEnemyMovement.SetGroundEnemySpeed(enemySpeed);
                }

                if (airEnemyMovements != null)
                {
                    airEnemyMovements.SetAirEnemySpeed(enemySpeed);
                }
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
