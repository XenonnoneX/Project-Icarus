using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowMission : MonoBehaviour
{
    [SerializeField] Image missionImage;
    // [SerializeField] TMP_Text missionRewardText;

    public void SetMission(Mission mission)
    {
        missionImage.sprite = mission.missionData.missionTextImage;
        // missionRewardText.text = (mission.missionReward * FindObjectOfType<MissionManager>().missionRewardMultiplyer).ToString("F0") + " RP";
    }
}