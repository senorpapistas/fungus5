using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField]
    private float balance;

    public void AddMoney(float amount)
    {
        balance += amount;
    }

    public float GetBalance()
    {
        return balance;
    }
}
