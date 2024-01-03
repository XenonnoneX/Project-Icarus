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

            string upgradeName = allUpgradeDatas[i].name;
            int upgradeLevel = PlayerPrefs.GetInt("Upgrade_" + upgradeName);

            upgrade.SetLevel(upgradeLevel);

            if (upgradeName == "MovSpeed") FindObjectOfType<PlayerMovement>().movSpeedMultiplier = upgrade.GetValue();
            else if (upgradeName == "RepairSpeed") FindObjectOfType<RepairTask>().repairSpeedMultiplier = upgrade.GetValue();
            else if (upgradeName == "RPStorageCap") FindObjectOfType<ResearchStation>().addedRPStorageCapacity = upgrade.GetValue();
            else if (upgradeName == "MoreRPFromMissions") FindObjectOfType<MissionManager>().missionRewardMultiplyer = upgrade.GetValue();
        }
    }
}
