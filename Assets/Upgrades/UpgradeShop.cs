using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    UpgradeShopManager upgradeManager;

    [SerializeField] AudioClip upgradeSound;

    public int currentFounds;

    public delegate void OnResearchPointsChanged();
    public OnResearchPointsChanged onFoundsChanged;

    private void Awake()
    {
        upgradeManager = GetComponent<UpgradeShopManager>();
    }

    private void Start()
    {
        currentFounds = PlayerPrefs.GetInt("TotalFounds");

        currentFounds += PlayerPrefs.GetInt("FoundsGained");

        onFoundsChanged += SaveResearchPoints;
        onFoundsChanged?.Invoke();
    }

    private void Update()
    {
        Hacks();
    }

    private void Hacks()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentFounds += 1000;
            onFoundsChanged?.Invoke();
        }
    }

    

    public void Buy(int index)
    {
        if (upgradeManager.UpgradeIsMaxLevel(index)) return;
        if (currentFounds < upgradeManager.GetUpgradeCost(index)) return;
        
        currentFounds -= upgradeManager.GetUpgradeCost(index);
        onFoundsChanged?.Invoke();
        upgradeManager.Upgrade(index);

        SoundManager.instance.PlaySound(upgradeSound);
    }

    void SaveResearchPoints()
    {
        PlayerPrefs.SetInt("TotalFounds", currentFounds);
    }
}
