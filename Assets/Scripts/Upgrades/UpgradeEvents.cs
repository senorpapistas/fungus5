using UnityEngine;
using System;

public class UpgradeEvents
{
    // Events
    public static event Action<float> FlashlightRangeIncreased;
    public static event Action<float> FlashlightAngleIncreased;
    //public static event Action<float> FlashlightDamageIncreased;
    public static event Action<float> PlayerMSIncreased;
    public static event Action<float> MoneyMultiplierIncreased; // still need to put in PlayerWallet.cs
    public static event Action<int> EnemySpawnsIncreased;
    //public static event Action<float> BombDropsIncreased;

    // Invoke Methods
    public static void OnFlashRangeInc(float amount) => FlashlightRangeIncreased?.Invoke(amount);
    public static void OnFlashAngleInc(float amount) => FlashlightAngleIncreased?.Invoke(amount);
    //public static void OnFlashDmgInc(float amount) => FlashlightDamageIncreased?.Invoke(amount);
    public static void OnPlayerMSInc(float amount) => PlayerMSIncreased?.Invoke(amount);
    public static void OnMoneyMultInc(float amount) => MoneyMultiplierIncreased?.Invoke(amount);
    public static void OnEnemySpawnInc(int amount) => EnemySpawnsIncreased?.Invoke(amount);
    //public static void OnBombDropInc(float amount) => BombDropsIncreased?.Invoke(amount);

}
