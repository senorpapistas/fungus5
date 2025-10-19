using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static event Action<float> MoneyAddEvent;

    [SerializeField]
    private float balance;

    public void AddMoney(float amount)
    {
        balance += amount;
        MoneyAddEvent?.Invoke(balance);
    }

    public float GetBalance()
    {
        return balance;
    }
}
