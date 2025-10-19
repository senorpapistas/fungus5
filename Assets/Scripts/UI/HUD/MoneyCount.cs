using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    public TMP_Text text;

    private void OnEnable()
    {
        PlayerWallet.MoneyAddEvent += OnMoneyAddEvent;
    }

    private void OnDisable()
    {
        PlayerWallet.MoneyAddEvent -= OnMoneyAddEvent;
    }

    private void OnMoneyAddEvent(float balance)
    {
        text.text = "$" + balance;
    }
}
