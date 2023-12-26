using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] List<UpgradeData> allUpgradeDatas;
    public List<Upgrade> allUpgrades;

    void Awake()
    {
        for (int i = 0; i < allUpgradeDatas.Count; i++)
        {
            Upgrade upgrade = new Upgrade(allUpgradeDatas[i]);
            allUpgrades.Add(upgrade);
            upgrade.SetLevel(PlayerPrefs.GetInt("Upgrade_" + allUpgradeDatas[i].name));
        }
    }
}
