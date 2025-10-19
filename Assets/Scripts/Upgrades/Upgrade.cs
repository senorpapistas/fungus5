using UnityEngine;
using System;

public class Upgrade
{
    public string Name { get; private set; }
    public float BasePrice { get; private set; }
    public float Rate { get; private set; }
    public float Growth { get; private set; }
    public int AmountOwned { get; private set; }

    public event Action OnUpgradePurchased;
    //public Action OnUpgrade { get; private set; }

    public Upgrade(string name, float basePrice, float rate, float growth)
    {
        Name = name;
        BasePrice = basePrice;
        Rate = rate;
        Growth = growth;
        AmountOwned = 0;
    }

    public float GetCurrentPrice()
    {
        return Mathf.Ceil(BasePrice * Mathf.Pow(Rate, Growth * AmountOwned));
    }

    public void Purchase()
    {
        AmountOwned++;
        //OnUpgrade?.Invoke();
        OnUpgradePurchased?.Invoke();
    }
}
