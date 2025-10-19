using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject healthUI;
    public GameObject healthIcon;

    public List<GameObject> healthIconList;

    private void OnEnable()
    {
        PlayerHealth.PlayerChangeHealthEvent += OnPlayerChangeHealthEvent;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerChangeHealthEvent -= OnPlayerChangeHealthEvent;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int size = Object.FindFirstObjectByType<PlayerHealth>().GetMaxHealth();
        UpdateUI(size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerChangeHealthEvent(int currentHealth)
    {
        UpdateUI(currentHealth);
    }

    void UpdateUI(int currentHealth)
    {
        //clear list
        foreach (var item in healthIconList)
        {
            Destroy(item.gameObject);
        }
        healthIconList.Clear();

        //make new list
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHealthIcon = Instantiate(healthIcon, healthUI.transform);
            healthIconList.Add(newHealthIcon);
        }
    }
}
