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
    [SerializeField] float smallEnemyHealth = 100f;
    [SerializeField] float mediumEnemyHealth = 100f;
    [SerializeField] float smallEnemySpeed = 1;
    [SerializeField] float mediumEnemySpeed = 1;
    [SerializeField] int smallEnemyDamage = 10;
    [SerializeField] int mediumEnemyDamage = 10;
    [SerializeField] float bossHealth = 1000f;
    [SerializeField] float bossSpeed = 1f;
    [SerializeField] int bossDamage = 50;
    


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

                // Now modify the stats of the spawned enemy based on its tag
                EnemyStat enemyStat = spawnedEnemy.GetComponent<EnemyStat>();

                if (enemyStat != null)
                {
                    // Check the enemy tag and apply the appropriate stats
                    if (spawnedEnemy.CompareTag("SmallEnemy"))
                    {
                        enemyStat.SetEnemyHealth(smallEnemyHealth);
                        enemyStat.SetEnemyDamage(smallEnemyDamage);
                    }
                    else if (spawnedEnemy.CompareTag("MediumEnemy"))
                    {
                        enemyStat.SetEnemyHealth(mediumEnemyHealth);
                        enemyStat.SetEnemyDamage(mediumEnemyDamage);
                    }
                    else if (spawnedEnemy.CompareTag("Boss"))
                    {
                        enemyStat.SetEnemyHealth(bossHealth);
                        enemyStat.SetEnemyDamage(bossDamage);
                    }
                }

                // Handle ground and air enemy movement speeds based on tag
                GroundEnemyMovements groundEnemyMovement = spawnedEnemy.GetComponent<GroundEnemyMovements>();
                AirEnemyMovements airEnemyMovements = spawnedEnemy.GetComponent<AirEnemyMovements>();

                if (spawnedEnemy.CompareTag("SmallEnemy"))
                {
                    if (groundEnemyMovement != null)
                    {
                        groundEnemyMovement.SetGroundEnemySpeed(smallEnemySpeed);
                    }
                    if (airEnemyMovements != null)
                    {
                        airEnemyMovements.SetAirEnemySpeed(smallEnemySpeed);
                    }
                }
                else if (spawnedEnemy.CompareTag("MediumEnemy"))
                {
                    if (groundEnemyMovement != null)
                    {
                        groundEnemyMovement.SetGroundEnemySpeed(mediumEnemySpeed);
                    }
                    if (airEnemyMovements != null)
                    {
                        airEnemyMovements.SetAirEnemySpeed(mediumEnemySpeed);
                    }
                }
                else if (spawnedEnemy.CompareTag("Boss"))
                {
                    if (groundEnemyMovement != null)
                    {
                        groundEnemyMovement.SetGroundEnemySpeed(bossSpeed);
                    }
                    if (airEnemyMovements != null)
                    {
                        airEnemyMovements.SetAirEnemySpeed(bossSpeed);
                    }
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
