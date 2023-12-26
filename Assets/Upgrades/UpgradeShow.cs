using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShow : MonoBehaviour
{
    public Button buyButton;
    [SerializeField] Image spriteImage;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text upgradeCostText;
    [SerializeField] TMP_Text nameText;

    public void SetUpgrade(Upgrade upgrade)
    {
        spriteImage.sprite = upgrade.upgradeData.shopIcon;
        levelText.text = "Level " + upgrade.currentLevel.ToString() + "/ " + (upgrade.upgradeData.levelValues.Count-1).ToString();
        if(upgrade.currentLevel < upgrade.upgradeData.upgradeCosts.Count) upgradeCostText.text = upgrade.upgradeData.upgradeCosts[upgrade.currentLevel].ToString() + " RP";
        else upgradeCostText.text = "MAX";
        nameText.text = upgrade.upgradeData.name;
    }
}
