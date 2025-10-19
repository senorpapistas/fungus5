using UnityEngine;

public class GameUpgrades : MonoBehaviour
{
    // hold reference to player to get flashlight and playerwallet, reference to enemy spawner


    // flashlight range, flashlight.cs
    // flashlight angle, flashlight.cs
    // flashlight damage, flashlight.cs
    // player movespeed, basicmovement.cs
    // money gain (high scaling) playerwallet.cs
    // max enemy spawns (high scaling) enemyspawner.cs
    // bomb drop rate increase (after done implementation) ?
    
    private void Start()
    {
        var flashlightRangeUpg = new Upgrade(
            "Flashlight Range",
            basePrice: 0,
            rate: 1f,
            growth: 1f
        );
        flashlightRangeUpg.OnUpgradePurchased += () => UpgradeEvents.OnFlashRangeInc(1f);
        UpgradeManager.Instance.RegisterUpgrade(flashlightRangeUpg);

        var flashlightAngleUpg = new Upgrade(
            "Flashlight Angle",
            basePrice: 0,
            rate: 1f,
            growth: 1f
        );
        flashlightAngleUpg.OnUpgradePurchased += () => UpgradeEvents.OnFlashAngleInc(1f);
        UpgradeManager.Instance.RegisterUpgrade(flashlightAngleUpg);

        var flashlightDmgUpg = new Upgrade(
            "Flashlight Damage",
            basePrice: 0,
            rate: 1f,
            growth: 1f
        );
        flashlightDmgUpg.OnUpgradePurchased += () => UpgradeEvents.OnFlashDmgInc(1f);
        UpgradeManager.Instance.RegisterUpgrade(flashlightDmgUpg);

        var playerMSUpg = new Upgrade(
            "Player Movespeed",
            basePrice: 0,
            rate: 1f,
            growth: 1f
        );
        playerMSUpg.OnUpgradePurchased += () => UpgradeEvents.OnPlayerMSInc(1f);
        UpgradeManager.Instance.RegisterUpgrade(playerMSUpg);

        var moneyGainUpg = new Upgrade(
            "Money Multiplier",
            basePrice: 0,
            rate: 1f,
            growth: 1f
        );
        moneyGainUpg.OnUpgradePurchased += () => UpgradeEvents.OnMoneyMultInc(1f);
        UpgradeManager.Instance.RegisterUpgrade(moneyGainUpg);

        var maxEnemySpawnsUpg = new Upgrade(
            "Max Enemy Spawns",
            basePrice: 0,
            rate: 1f,
            growth: 1f
        );
        maxEnemySpawnsUpg.OnUpgradePurchased += () => UpgradeEvents.OnEnemySpawnInc(1);
        UpgradeManager.Instance.RegisterUpgrade(maxEnemySpawnsUpg);

        //var bombDropRateUpg = new Upgrade(
        //    "Bomb Drop Rate",
        //    basePrice: 0,
        //    rate: 1f,
        //    growth: 1f
        //);
        //bombDropRateUpg.OnUpgradePurchased += () => UpgradeEvents.OnBombDropInc(1f);
        //UpgradeManager.Instance.RegisterUpgrade(bombDropRateUpg);
    }
}

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Flashlight Range",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Flashlight Angle",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Flashlight Damage",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Player Speed",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Money Multiplier",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Max Enemy Spawns",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));

//UpgradeManager.Instance.RegisterUpgrade(new Upgrade(
//    "Bomb Drop Rate Increase",
//    basePrice: 0,
//    rate: 1f,
//    growth: 1f,
//    onUpgrade: () => Debug.Log("fat")
//));