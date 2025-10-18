using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public Flashlight playerFlashlight;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 30f;
    public int maxEnemies = 5;
    public float spawnInterval = 1f;
    public LayerMask spawnAreaMask;

    private List<Enemy> activeEnemies = new List<Enemy>();
    private bool canSpawn = true;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (playerFlashlight == null)
            playerFlashlight = player.GetComponentInChildren<Flashlight>();

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            if (activeEnemies.Count < maxEnemies)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();
                if (spawnPosition != Vector3.zero)
                {
                    SpawnEnemy(spawnPosition);
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 100; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            Vector3 position = player.position + offset;

            // Check if inside flashlight beam and on valid ground
            if (IsValidSpawnPosition(position))
            {
                return position;
            }
        }
        Debug.Log("failed to find a valid spawn position, defaulting to 0");
        return Vector3.zero;
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        // Check if position is in flashlight beam
        if (playerFlashlight.IsObjectInBeam(position)) return false;

        // Check for ground
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 2, Vector3.down, out hit, 4f, spawnAreaMask))
        {
            return true;
        }
        return false;
    }

    private void SpawnEnemy(Vector3 position)
    {
        GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            activeEnemies.Add(enemy);
            StartCoroutine(MonitorEnemy(enemy));
        }
    }

    // Make spawner in charge of handling count so object doesn't have to tell spawner its been collected
    private IEnumerator MonitorEnemy(Enemy enemy)
    {
        while (enemy != null)
        {
            yield return new WaitForSeconds(0.5f);
        }
        activeEnemies.Remove(enemy);
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}