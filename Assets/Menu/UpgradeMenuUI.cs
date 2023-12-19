using TMPro;
using UnityEngine;

public class UpgradeMenuUI : MonoBehaviour
{
    UpgradeMenu upgradeMenu;

    [SerializeField] TMP_Text rpText;

    private void Awake()
    {
        upgradeMenu = GetComponent<UpgradeMenu>();

        upgradeMenu.onResearchPointsChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        rpText.text = upgradeMenu.currentResearchPoints.ToString() + " RP";
    }
}