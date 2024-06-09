using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  
    public Transform spawnArea;        
    public float initialSpawnInterval = 5f;  
    public float intervalChangeTime = 10f;   
    public float minSpawnInterval = 0.5f;     

    private float currentSpawnInterval;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2),
                0
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(currentSpawnInterval);

            if (Time.timeSinceLevelLoad >= intervalChangeTime)
            {
                currentSpawnInterval = Mathf.Max(currentSpawnInterval - 0.1f, minSpawnInterval);
                intervalChangeTime += 10f;
            }
        }
    }
}
