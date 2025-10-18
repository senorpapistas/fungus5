using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public event Action HealthChange;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Flashlight")) { ChangeHealth(-1); }
    }

    private void Update()
    {
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeHealth(int change)
    {
        currentHealth += change;
        HealthChange?.Invoke();
    }
}
