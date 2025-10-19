using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static event Action<float> MoneyChangeEvent;

    [SerializeField]
    private float balance;

    private float moneyMultiplier = 1f;

    private void OnEnable()
    {
        UpgradeEvents.MoneyMultiplierIncreased += IncreaseMoneyMultiplier;
    }

    private void OnDisable()
    {
        UpgradeEvents.MoneyMultiplierIncreased -= IncreaseMoneyMultiplier;
    }

    private void OnValidate()
    {
        MoneyChangeEvent?.Invoke(balance);
    }

    public void AddMoney(float amount)
    {
        float final_amount = amount * moneyMultiplier;
        balance += final_amount;
        MoneyChangeEvent?.Invoke(balance);
    }

    public float GetBalance()
    {
        return balance;
    }
    private void IncreaseMoneyMultiplier(float multiplier)
    {
        moneyMultiplier *= multiplier;
    }
}
