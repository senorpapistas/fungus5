using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public Flashlight playerFlashlight;

    [Header("Spawn Distances")]
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 30f;
    public Room room;

    [Header("Spawn Settings")]
    public int maxEnemies = 5;
    public int enemiesSpawned = 0;
    public int totalEnemiesToSpawn = 5;
    public float spawnInterval = 1f;
    public LayerMask spawnAreaMask;

    public List<Enemy> activeEnemies = new List<Enemy>();

    [Header("States")]
    public bool canSpawn = true;
    public bool finishedSpawning = false;

    private void Start()
    {
        if (room)
        {
            maxSpawnDistance = room.planeMesh.bounds.size.x/2 - room.planeMesh.bounds.size.x*.2f;
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (playerFlashlight == null)
            playerFlashlight = player.GetComponentInChildren<Flashlight>();

        StartCoroutine(SpawnRoutine());
    }

    public void StartSpawn()
    {
        canSpawn = true;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (finishedSpawning && activeEnemies.Count == 0 && room)
        {
            room.ClearRoom();
            this.gameObject.SetActive(false);
        }
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

                if (enemiesSpawned == totalEnemiesToSpawn)
                {
                    Debug.Log("stopped spawning");

                    finishedSpawning = true;
                    yield break;
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        // Spawn enemy without spawn distance bounds and in fog of war
        for (int i = 0; i < 20; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

            Vector3 position = player.position + offset;
            if (room) { position = new Vector3 (room.transform.position.x, player.position.y, room.transform.position.z) + offset; }

            // Check if inside flashlight beam and on valid ground
            if (IsValidSpawnPosition(position))
            {
                return position;
            }
        }
        // Assuming flashlight covers nearly the whole map
        Debug.Log("failed to find a valid spawn position, defaulting to lenient search");
        // Spawn enemy within flashlight beam if possible, else zero
        for (int i = 0; i < 20; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minSpawnDistance - 2, maxSpawnDistance + 10);
            Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            Vector3 position = player.position + offset;

            // Check if inside flashlight beam and on valid ground
            if (IsLenientSpawnPosition(position))
            {
                return position;
            }
        }
        Debug.Log("failed to find a lenient spawn position, defaulting to vector 0");
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

    private bool IsLenientSpawnPosition(Vector3 position)
    {
        // Only check for ground
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
            enemiesSpawned++;
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

    private void OnEnable()
    {
        UpgradeEvents.EnemySpawnsIncreased += IncreaseSpawn;
    }

    private void OnDisable()
    {
        UpgradeEvents.EnemySpawnsIncreased -= IncreaseSpawn;

        foreach (Enemy enemy in activeEnemies)
        {
            Destroy(enemy);
        }
    }

    private void IncreaseSpawn(int amount)
    {
        maxEnemies += 1;
        if (spawnInterval > 0.1f)
        {
            spawnInterval -= 0.1f;
        }
    }
}