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

            if (upgradeName == "Movement Speed") FindObjectOfType<PlayerMovement>().movSpeedMultiplier = upgrade.GetValue();
            else if (upgradeName == "Repair Speed") FindObjectOfType<RepairTask>().repairSpeedMultiplier = upgrade.GetValue();
            else if (upgradeName == "Analysis Speed") FindObjectOfType<AnomalyAnalysisStation>().analysisSpeedMultiplier = upgrade.GetValue();
            else if (upgradeName == "Mission Recharge Speed") FindObjectOfType<MissionManager>().missionRechargeMultiplier = upgrade.GetValue();
            else if (upgradeName == "Pull-Up Force") FindObjectOfType<SpaceShipMovement>().pullUpSpeedMultiplier = upgrade.GetValue();
            else if (upgradeName == "Research Capacity") FindObjectOfType<ResearchStation>().addedRPStorageCapacity = upgrade.GetValue();
            else if (upgradeName == "Station Health")
            {
                ControlStation[] stations = FindObjectsOfType<ControlStation>();

                foreach (ControlStation station in stations)
                {
                    station.timeToBreakMultiplier = upgrade.GetValue();
                }
            }
            else if (upgradeName == "Research Efficiency") FindObjectOfType<MissionManager>().missionRewardMultiplyer = upgrade.GetValue();
            else if (upgradeName == "Starter Repair Kit" && upgradeLevel > 0) FindObjectOfType<PlayerInventory>().GetRepairKit();
            else if (upgradeName == "Space Suit" && upgradeLevel > 0) FindObjectOfType<PlayerMovement>().ActivateSpaceSuit();
        }
    }
}
