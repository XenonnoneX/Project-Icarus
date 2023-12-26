using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
    public Sprite shopIcon;
    public List<float> levelValues;
    public List<int> upgradeCosts;
}
