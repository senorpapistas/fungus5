using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Image bg;
    public Image healthbar;
    public EnemyHealth enemyHealth;

    private void OnEnable()
    {
        enemyHealth.HealthChangeEvent += OnHealthChangeEvent;
    }

    private void OnDisable()
    {
        enemyHealth.HealthChangeEvent -= OnHealthChangeEvent;
    }

    private void Start()
    {
        bg.enabled = false;
        healthbar.enabled = false;
    }

    void OnHealthChangeEvent()
    {
        bg.enabled = true;
        healthbar.enabled = true;
        healthbar.fillAmount = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;

        StopAllCoroutines();
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(1f);
        bg.enabled = false;
        healthbar.enabled = false;
    }
}
