using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenuUI : MonoBehaviour
{
    UpgradeShop upgradeShop;
    UpgradeShopManager upgradeManager;

    [SerializeField] TMP_Text rpText;
    [SerializeField] GameObject UpgradeShowParent;
    [SerializeField] UpgradeShow upgradeShowPrefab;

    List<UpgradeShow> upgradeShowObjects = new List<UpgradeShow>();

    private void Awake()
    {
        upgradeShop = GetComponent<UpgradeShop>();
        upgradeManager = GetComponent<UpgradeShopManager>();

        upgradeShop.onFoundsChanged += UpdateUI;
        upgradeManager.onUpgradesChanged += UpdateUI;
    }

    private void Start()
    {
        SpawnUpgradeShowObjects();
        
        UpdateUI();
    }

    public void Buy(int index)
    {
        upgradeShop.Buy(index);
    }

    private void UpdateUI()
    {
        rpText.text = upgradeShop.currentFounds.ToString() + " €";

        for (int i = 0; i < upgradeShowObjects.Count; i++)
        {
            upgradeShowObjects[i].SetUpgrade(upgradeManager.allUpgrades[i]);
        }
    }

    private void SpawnUpgradeShowObjects()
    {
        for (int i = 0; i < upgradeManager.allUpgrades.Count; i++)
        {
            int index = i; // Create a local variable to capture the correct value of i
            UpgradeShow temp = Instantiate(upgradeShowPrefab, UpgradeShowParent.transform);
            temp.SetUpgrade(upgradeManager.allUpgrades[i]);
            temp.buyButton.onClick.AddListener(() => Buy(index));
            upgradeShowObjects.Add(temp);
        }
    }
}