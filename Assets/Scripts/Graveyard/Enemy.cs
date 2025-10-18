using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IMoveable, ILootable
{
    public float moveSpeed = 2f;
    public float minDistanceToPlayer = 1f;
    public float timeToKill = 3f;
    public GameObject moneyPrefab;
    public float moneyValue = 10f;

    private Transform player;
    private bool isStunned;
    private float stunTimer;
    private Vector3 lastPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (!isStunned && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            // Default behavior as of now is to move directly at the player very slowly
            if (Vector3.Distance(transform.position, player.position) > minDistanceToPlayer)
            {
                Move(direction);
            }
        }
    }

    public void OnFlashlightHit()
    {
        // Continuously stun enemy during flashlight shine
        if (!isStunned)
        {
            isStunned = true;
            StartCoroutine(StunCountdown());
        }
    }

    private IEnumerator StunCountdown()
    {
        stunTimer = 0f;
        while (stunTimer < timeToKill)
        {
            stunTimer += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"Stun timer is creature than time to kill! Dropping loot");
        DropLoot(transform.position);
    }

    public void OnFlashlightExit()
    {
        // Resume enemy movement
        isStunned = false;
        StopAllCoroutines();
    }

    public void Move(Vector3 direction)
    {
        // Added delta time
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void Stop()
    {
        isStunned = true;
    }

    public void DropLoot(Vector3 position)
    {
        if (moneyPrefab != null)
        {
            GameObject money = Instantiate(moneyPrefab, position, Quaternion.identity); //  + Vector3.up
            Debug.Log($"Enemy killed! Dropping loot at {position + Vector3.up}");
            Money moneyComponent = money.GetComponent<Money>();
            if (moneyComponent != null)
            {
                moneyComponent.SetValue(moneyValue);
            }
            // Destroy enemy
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("prefab error in Enemy.cs you should not get here");
        }
    }
}