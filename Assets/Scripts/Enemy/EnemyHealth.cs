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

    public void ChangeHealth(int change)
    {
        currentHealth += change;
        HealthChange?.Invoke();
    }
}
