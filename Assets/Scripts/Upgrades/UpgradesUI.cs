using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class UpgradesUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject upgradeButtonPrefab;
    public Transform upgradesContainer;

    private Dictionary<string, Button> upgradeButtons = new Dictionary<string, Button>();

    private void Start()
    {
        foreach (var upgrade in UpgradeManager.Instance.GetAllUpgrades())
        {
            CreateUpgradeButton(upgrade);
        }
    }

    private void CreateUpgradeButton(Upgrade upgrade)
    {
        GameObject buttonObj = Instantiate(upgradeButtonPrefab, upgradesContainer);
        Button button = buttonObj.GetComponent<Button>();

        Text buttonText = buttonObj.GetComponentInChildren<Text>();
        buttonText.text = $"{upgrade.Name}\nCost: {upgrade.GetCurrentPrice()}";

        button.onClick.AddListener(() =>
        {
            TryPurchaseUpgrade(upgrade, buttonText);
        });

        upgradeButtons[upgrade.Name] = button;

    }

    private void TryPurchaseUpgrade(Upgrade upgrade, Text buttonText)
    {
        float price = upgrade.GetCurrentPrice();

        if (true) // Check the player wallet if the player can afford 
        {
            // take away the money in player wallet
            upgrade.Purchase();

            buttonText.text = $"{upgrade.Name}\nCost: {upgrade.GetCurrentPrice()}";
        }
        else
        {
            Debug.Log($"not enough money to purchase {upgrade.Name}");
        }
    }
}
