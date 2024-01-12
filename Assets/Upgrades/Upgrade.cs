
[System.Serializable]
public class Upgrade
{
    public UpgradeData upgradeData;
    public int currentLevel;
    public Upgrade(UpgradeData uData, int level = 0)
    {
        upgradeData = uData;
        currentLevel = level;
    }

    public void LevelUp()
    {
        currentLevel++;
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    internal float GetValue()
    {
        return upgradeData.levelValues[currentLevel];
    }
}
