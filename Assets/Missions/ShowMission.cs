using TMPro;
using UnityEngine;

public class ShowMission : MonoBehaviour
{
    [SerializeField] TMP_Text missionText;
    [SerializeField] TMP_Text missionRewardText;

    public void SetMission(Mission mission)
    {
        missionText.text = mission.missionText;
        missionRewardText.text = (mission.missionReward * FindObjectOfType<MissionManager>().missionRewardMultiplyer).ToString("F0") + " RP";
    }
}