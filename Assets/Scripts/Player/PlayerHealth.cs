using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> PlayerChangeHealthEvent;

    public static event Action<int> PlayerChangeMaxHealthEvent;

    public bool takeDamage;

    [SerializeField]
    public int maxHealth, currentHealth;
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

            AudioManager.Instance.PlaySound("meow");
        }
    }

    public void ChangeMaxHealth(int change)
    {
        maxHealth += change;
        PlayerChangeMaxHealthEvent?.Invoke(maxHealth);
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
