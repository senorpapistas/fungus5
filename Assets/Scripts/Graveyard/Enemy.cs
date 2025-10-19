using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour, IMoveable, ILootable
{
    public float moveSpeed = 2f;
    public float minDistanceToPlayer = 1f;
    //public float timeToKill = 3f;
    public EnemyHealth health;
    public GameObject moneyPrefab;
    public GameObject explosionPrefab;
    public float moneyValue = 10f;

    //private float damagePerHit = 4f;

    private Transform player;
    private bool isStunned;
    //private float stunTimer;
    private Vector3 lastPosition;

    public static event Action EnemyDeathEvent;

    public GenericLootDropTableGameObject lootDropTable;
    public int num_drops;

    [SerializeField] private GameObject atkHB;
    [SerializeField] private bool atkCD;

    private Coroutine stunCR;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = transform.position;
        lootDropTable.ValidateTable();
        atkCD = true;
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
        if (Vector3.Distance(transform.position, player.position) <= minDistanceToPlayer)
        {
            Debug.Log("itchy enemy nuts");
            if (atkCD)
            {
                Debug.Log("spittin on that thang");
                atkHB.SetActive(true);
                atkCD = false;
                atkHB.GetComponent<EnemyAttack>().Spit();
                StartCoroutine(AttackCooldown());
            }
        }
    }

    public void OnFlashlightHit()
    {
        // Continuously stun enemy during flashlight shine
        if (!isStunned)
        {
            isStunned = true;
            stunCR = StartCoroutine(StunCountdown());
        }
    }

    private IEnumerator StunCountdown()
    {
        //stunTimer = 0f;
        //while (stunTimer < timeToKill)
        float timer = 0f;
        while (health.currentHealth > 0)
        {
            //stunTimer += Time.deltaTime;
            timer += Time.deltaTime;
            if (timer > .1f) { health.ChangeHealth(-4); timer = 0f; }
            yield return null;
        }
        Debug.Log($"Stun timer is creature than time to kill! Dropping loot");
        //DropLoot(transform.position);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("atkCD done");
        atkHB.SetActive(false);
        atkCD = true;
    }

    public void OnFlashlightExit()
    {
        // Resume enemy movement
        isStunned = false;
        if (stunCR != null)
        {
            StopCoroutine(stunCR);
        }
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
            //Debug.Log($"Enemy killed! Dropping loot at {position + Vector3.up}");
            Money moneyComponent = money.GetComponent<Money>();
            if (moneyComponent != null)
            {
                moneyComponent.SetValue(moneyValue);
            }

            GenericLootDropItemGameObject drop = lootDropTable.PickLootDropItem();
            GameObject loot = Instantiate(drop.item, position, Quaternion.identity);

            // Explosion
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            Destroy(explosion, 2f);

            // Destroy enemy
            Destroy(gameObject);

            //event call
            EnemyDeathEvent?.Invoke();
        }
        else
        {
            Debug.Log("prefab error in Enemy.cs you should not get here");
        }
    }

    void OnValidate()
    {
        lootDropTable.ValidateTable();
    }
}