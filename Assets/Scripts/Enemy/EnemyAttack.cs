using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDamage(other);
    }

    void TryDamage(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.LoseHealth(damage);
        }
    }
}
