using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Image bg;
    public Image healthbar;
    public EnemyHealth enemyHealth;

    private void OnEnable()
    {
        enemyHealth.HealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        enemyHealth.HealthChange -= OnHealthChange;
    }

    private void Start()
    {
        bg.enabled = false;
        healthbar.enabled = false;
    }

    void OnHealthChange()
    {
        bg.enabled = true;
        healthbar.enabled = true;
        healthbar.fillAmount = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;
    }
}
