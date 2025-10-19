using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject spitFX;

    public void Spit()
    {
        GameObject spit = Instantiate(spitFX, transform, false);
        spit.transform.localPosition = Vector3.zero - Vector3.forward * 0.5f;
        spit.transform.localRotation = Quaternion.identity;
        spit.transform.localScale = Vector3.one * 1.5f;
        Destroy(spit, 2f);
    }

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
