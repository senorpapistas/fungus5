using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static event Action<float> MoneyChangeEvent;

    [SerializeField]
    private float balance;

    private void OnValidate()
    {
        MoneyChangeEvent?.Invoke(balance);
    }

    public void AddMoney(float amount)
    {
        balance += amount;
        MoneyChangeEvent?.Invoke(balance);
    }

    public float GetBalance()
    {
        return balance;
    }
}
