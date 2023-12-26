using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    UpgradeShopManager upgradeManager;

    public int currentResearchPoints;

    public delegate void OnResearchPointsChanged();
    public OnResearchPointsChanged onResearchPointsChanged;

    private void Awake()
    {
        upgradeManager = GetComponent<UpgradeShopManager>();
    }

    private void Start()
    {
        currentResearchPoints = PlayerPrefs.GetInt("TotalResearchPoints");

        currentResearchPoints += PlayerPrefs.GetInt("CollectedResearchPoints");

        onResearchPointsChanged += SaveResearchPoints;
        onResearchPointsChanged?.Invoke();
    }

    private void Update()
    {
        Hacks();
    }

    private void Hacks()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentResearchPoints += 1000;
            onResearchPointsChanged?.Invoke();
        }
    }

    public void Buy(int index)
    {
        if (upgradeManager.UpgradeIsMaxLevel(index)) return;
        if (currentResearchPoints < upgradeManager.GetUpgradeCost(index)) return;
        
        currentResearchPoints -= upgradeManager.GetUpgradeCost(index);
        onResearchPointsChanged?.Invoke();
        upgradeManager.Upgrade(index);
    }

    void SaveResearchPoints()
    {
        PlayerPrefs.SetInt("TotalResearchPoints", currentResearchPoints);
    }
}
