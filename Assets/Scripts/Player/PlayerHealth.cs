using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> PlayerChangeHealthEvent;

    public bool takeDamage;

    [SerializeField]
    private int currentHealth, maxHealth;
    [SerializeField]
    private float InvincibilityTime;

    [Header("State")]
    public bool invincible;

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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        PlayerChangeHealthEvent?.Invoke(currentHealth);
    }

    public void LoseHealth(int change)
    {
        if (!invincible)
        {
            currentHealth -= change;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            PlayerChangeHealthEvent?.Invoke(currentHealth);

            StartInvincibility();
        }
    }

    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }


    private void StartInvincibility()
    {
        StartCoroutine(Invincibility());
    }

    IEnumerator Invincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(InvincibilityTime);
        invincible = false;
    }
}
