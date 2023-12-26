using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    MissionManager missionManager;

    [SerializeField] ShowMission showMission;

    List<ShowMission> showMissions = new List<ShowMission>();

    private void Awake()
    {
        missionManager = FindObjectOfType<MissionManager>();
        missionManager.onMissionsChanged += UpdateUI;
    }

    private void Start()
    {
        SpawnShowMissionObjects();
        UpdateUI();
    }

    private void SpawnShowMissionObjects()
    {
        for (int i = 0; i < missionManager.maxMissions; i++)
        {
            showMissions.Add((ShowMission)Instantiate(showMission, transform));
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < missionManager.maxMissions; i++)
        {
            if(missionManager.currentMissions.Count <= i)
            {
                showMissions[i].gameObject.SetActive(false);
                continue;
            }
            else
            {
                showMissions[i].SetMission(missionManager.currentMissions[i]);
                showMissions[i].gameObject.SetActive(true);
            }
        }
    }

}
