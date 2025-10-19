using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    public TMP_Text text;

    private void OnEnable()
    {
        PlayerWallet.MoneyChangeEvent += OnMoneyChangeEvent;
    }

    private void OnDisable()
    {
        PlayerWallet.MoneyChangeEvent -= OnMoneyChangeEvent;
    }

    private void OnMoneyChangeEvent(float balance)
    {
        text.text = "$" + balance;
    }
}
