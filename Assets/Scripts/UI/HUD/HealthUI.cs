using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject healthUI;
    public GameObject maxHealthUI;

    [Header("Icon Prefab")]
    public GameObject healthIcon;

    [Space(10)]
    public List<GameObject> healthIconList;

    public List<GameObject> maxHealthIconList;

    private void OnEnable()
    {
        PlayerHealth.PlayerChangeHealthEvent += UpdateHealth;
        PlayerHealth.PlayerChangeMaxHealthEvent += UpdateMaxHealth;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerChangeHealthEvent -= UpdateHealth;
        PlayerHealth.PlayerChangeMaxHealthEvent -= UpdateMaxHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        UpdateHealth(playerHealth.GetMaxHealth());
        UpdateMaxHealth(playerHealth.GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth(int currentHealth)
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

    void UpdateMaxHealth(int maxHealth)
    {
        //clear list
        foreach (var item in maxHealthIconList)
        {
            Destroy(item.gameObject);
        }
        maxHealthIconList.Clear();

        //make new list
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHealthIcon = Instantiate(healthIcon, maxHealthUI.transform);
            newHealthIcon.GetComponent<Image>().color = Color.black;
            maxHealthIconList.Add(newHealthIcon);
        }
    }
}
