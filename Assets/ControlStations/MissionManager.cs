using System.Collections.Generic;
using UnityEngine;

public class MissionManager : ControlStation
{
    ResearchStation research;

    public int maxMissions = 3;
    [SerializeField] float timeToGetNewMission = 10f;
    float newMissionTimer;

    [HideInInspector] public float missionRewardMultiplyer = 1f;

    [SerializeField] List<MissionData> allMissions;
    public List<Mission> currentMissions = new List<Mission>();
    [SerializeField] List<Mission> completedMissions = new List<Mission>();

    public delegate void OnMissionsChanged();
    public OnMissionsChanged onMissionsChanged;

    private void Awake()
    {
        research = FindObjectOfType<ResearchStation>();
    }

    protected override void Update()
    {
        base.Update();

        CheckMissionsCompleted();

        if (stationState != StationState.Working) return;

        if(currentMissions.Count * timeToGetNewMission + newMissionTimer < maxMissions * timeToGetNewMission) newMissionTimer += Time.deltaTime * timeScale;
    }

    public override void CompleteTask()
    {
        GetNewMissions();

        base.CompleteTask();
    }

    private void GetNewMissions()
    {
        while (newMissionTimer > timeToGetNewMission && currentMissions.Count < maxMissions && currentMissions.Count < allMissions.Count)
        {
            print("Getting new mission");
            newMissionTimer -= timeToGetNewMission;

            currentMissions.Add(GetNewRandomMission());
            onMissionsChanged.Invoke();
        }
    }

    private Mission GetNewRandomMission()
    {
        int breakCounter = 0;
        int rand = -1;
        
        while (rand == -1 || MissionAlreadyActive(rand) || breakCounter > 100)
        {
            rand = UnityEngine.Random.Range(0, allMissions.Count);
            breakCounter++;
        }

        return new Mission(allMissions[rand]);
    }

    private bool MissionAlreadyActive(int rand)
    {
        Mission checkMission = new Mission(allMissions[rand]);

        foreach (Mission mission in currentMissions)
        {
            if (mission.missionData == checkMission.missionData)
            {
                return true;
            }
        }

        return false;
    }

    private void CheckMissionsCompleted()
    {
        foreach (Mission mission in currentMissions)
        {
            print("Checking Mission: " + mission.missionData.name);
            if (mission.CheckMissionCompleted())
            {
                research.AddResearchPoints(mission.missionReward * missionRewardMultiplyer);
                completedMissions.Add(mission);
                print("Mission Completed: " + mission.missionData.name);
            }
        }

        foreach (Mission mission in completedMissions)
        {
            if (currentMissions.Contains(mission))
            {
                print("Removing Mission: " + mission.missionData.name + " from current missions");
                currentMissions.Remove(mission);
                onMissionsChanged.Invoke();
            }
        }
    }

    public bool MissionAvailable()
    {
        return newMissionTimer > timeToGetNewMission;
    }
}
