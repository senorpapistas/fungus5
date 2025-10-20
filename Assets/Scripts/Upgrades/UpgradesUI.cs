using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
public class UpgradesUI : MenuClass
{
    [Header("UI Elements")]
    public GameObject upgradeButtonPrefab;
    public Transform upgradesContainer;

    private PlayerWallet playerWallet;

    private Dictionary<string, Button> upgradeButtons = new Dictionary<string, Button>();

    private void Start()
    {
        base.Start();

        StartCoroutine(InitializeUpgUI());

        //foreach (var upgrade in UpgradeManager.Instance.GetAllUpgrades())
        //{
        //    CreateUpgradeButton(upgrade);
        //}

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if (player != null)
        //{
        //    playerWallet = player.GetComponent<PlayerWallet>();
        //}
    }

    private IEnumerator InitializeUpgUI()
    {
        yield return new WaitForEndOfFrame();
        foreach (var upgrade in UpgradeManager.Instance.GetAllUpgrades())
        {
            CreateUpgradeButton(upgrade);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerWallet = player.GetComponent<PlayerWallet>();
        }
    }

    private void CreateUpgradeButton(Upgrade upgrade)
    {
        GameObject buttonObj = Instantiate(upgradeButtonPrefab, upgradesContainer);
        Button button = buttonObj.GetComponent<Button>();

        TMP_Text nameText = buttonObj.transform.Find("Name").GetComponent<TMP_Text>();
        TMP_Text ownedText = buttonObj.transform.Find("Count").GetComponent<TMP_Text>();
        TMP_Text priceText = buttonObj.transform.Find("Price").GetComponent<TMP_Text>();
        nameText.text = upgrade.Name;
        ownedText.text = $"+{upgrade.AmountOwned}";
        priceText.text = $"${upgrade.GetCurrentPrice()}";

        //Text buttonText = buttonObj.GetComponentInChildren<Text>();
        //buttonText.text = $"{upgrade.Name}\nCost: {upgrade.GetCurrentPrice()}";

        button.onClick.AddListener(() =>
        {
            TryPurchaseUpgrade(upgrade, priceText, ownedText);
        });

        upgradeButtons[upgrade.Name] = button;

    }

    private void TryPurchaseUpgrade(Upgrade upgrade, TMP_Text newText, TMP_Text newOwned)
    {
        float price = upgrade.GetCurrentPrice();

        // Check the player wallet if the player can afford 
        if (playerWallet != null && playerWallet.GetBalance() >= price) 
        {
            // take away the money in player wallet
            playerWallet.AddMoney(-price);
            upgrade.Purchase();

            newText.text = $"${upgrade.GetCurrentPrice()}";
            newOwned.text = $"+{upgrade.AmountOwned}";
        }
        else
        {
            Debug.Log($"not enough money to purchase {upgrade.Name}");
        }
    }
}
