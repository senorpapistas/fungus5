using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    //public List<Upgrade> upgradesList = new List<Upgrade>();

    public Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //InitializeUpgrades();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //private void InitializeUpgrades()
    //{
    //    foreach (var upgrade in upgradesList)
    //    {
    //        upgrades.Add(upgrade.Name, upgrade);
    //    }
    //}
    
    public void RegisterUpgrade(Upgrade upgrade)
    {
        if (!upgrades.ContainsKey(upgrade.Name))
        {
            upgrades.Add(upgrade.Name, upgrade);
            //upgradesList.Add(upgrade);
        }
    }

    public Upgrade GetUpgrade(string name)
    {
        if (upgrades.ContainsKey(name))
        {
            return upgrades[name];
        }
        return null;
    }

    public IEnumerable<Upgrade> GetAllUpgrades()
    {
        return upgrades.Values;
    }
}
