using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> PlayerChangeHealthEvent;

    public bool takeDamage;

    [SerializeField]
    private int currentHealth, maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (takeDamage)
        {
            takeDamage = false;
            LoseHealth(3);
        }
    }

    public void GainHealth(int change)
    {
        currentHealth += change;
        Mathf.Clamp(currentHealth, 0, maxHealth);
        PlayerChangeHealthEvent?.Invoke(currentHealth);
    }

    public void LoseHealth(int change)
    {
        currentHealth -= change;
        Mathf.Clamp(currentHealth, 0, maxHealth);
        PlayerChangeHealthEvent?.Invoke(currentHealth);
    }

    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }
}
