using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopManager : MonoBehaviour
{
    [SerializeField] List<UpgradeData> allUpgradeDatas;
    public List<Upgrade> allUpgrades;

    public delegate void OnUpgradeChanged();
    public OnUpgradeChanged onUpgradesChanged;

    void Awake()
    {
        for (int i = 0; i < allUpgradeDatas.Count; i++)
        {
            Upgrade upgrade = new Upgrade(allUpgradeDatas[i]);
            allUpgrades.Add(upgrade);
            upgrade.SetLevel(PlayerPrefs.GetInt("Upgrade_" + allUpgradeDatas[i].name));
        }
        onUpgradesChanged += SaveUpgrades;
    }

    public bool UpgradeIsMaxLevel(int index)
    {
        if (allUpgrades[index].currentLevel == allUpgrades[index].upgradeData.levelValues.Count - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal int GetUpgradeCost(int index)
    {
        return allUpgradeDatas[index].upgradeCosts[allUpgrades[index].currentLevel];
    }

    internal void Upgrade(int index)
    {
        allUpgrades[index].LevelUp();
        onUpgradesChanged.Invoke();
    }

    void SaveUpgrades()
    {
        for (int i = 0; i < allUpgrades.Count; i++)
        {
            PlayerPrefs.SetInt("Upgrade_" + allUpgradeDatas[i].name, allUpgrades[i].currentLevel);
        }
    }
}