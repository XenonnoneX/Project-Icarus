using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : ControlStation
{
    ResearchStation research;

    public int maxMissions = 3;
    public int missionsForArtifact = 1;
    [SerializeField] float timeToGetNewMission = 10f;
    float newMissionTimer;

    [HideInInspector] public float missionRewardMultiplyer = 1f;

    [SerializeField] List<MissionData> allMissions;
    public List<Mission> currentMissions = new List<Mission>();
    [SerializeField] List<Mission> completedMissions = new List<Mission>();
    public int CompletedMissionsCount => completedMissions.Count;

    public delegate void OnMissionsChanged();
    public OnMissionsChanged onMissionsChanged;

    public delegate void OnMissionCompleted();
    public OnMissionCompleted onMissionCompleted;

    private void Awake()
    {
        research = FindObjectOfType<ResearchStation>();
    }

    protected override void Start()
    {
        base.Start();
        newMissionTimer = timeToGetNewMission;
    }

    protected override void Update()
    {
        base.Update();

        CheckMissionsCompleted();

        if (stationState != StationState.Working) return;

        if(currentMissions.Count * timeToGetNewMission + newMissionTimer < maxMissions * timeToGetNewMission) newMissionTimer += Time.deltaTime * timeScale;
    }

    public override void CompleteTask(ItemData currentItem = null)
    {
        GetNewMissions();

        base.CompleteTask(currentItem);
    }

    private void GetNewMissions()
    {
        while (newMissionTimer > timeToGetNewMission && currentMissions.Count < maxMissions && currentMissions.Count < allMissions.Count)
        {
            newMissionTimer -= timeToGetNewMission;

            Mission newMission = GetNewRandomMission();

            newMission.Setup();
            currentMissions.Add(newMission);
            onMissionsChanged.Invoke();
        }
    }

    private Mission GetNewRandomMission()
    {
        int breakCounter = 0;
        int rand = -1;
        
        while (rand == -1 || MissionTypeActive(rand) || breakCounter > 100)
        {
            rand = UnityEngine.Random.Range(0, allMissions.Count);
            breakCounter++;
        }

        return new Mission(allMissions[rand]);
    }

    private bool MissionTypeActive(int rand)
    {
        Mission checkMission = new Mission(allMissions[rand]);

        foreach (Mission mission in currentMissions)
        {
            if (mission.missionData == checkMission.missionData || SameStationMissions(mission, checkMission))
            {
                return true;
            }
        }

        return false;
    }

    private bool SameStationMissions(Mission mission, Mission checkMission)
    {
        bool sameMisionType = mission.missionData.missionSteps[0].missionType == checkMission.missionData.missionSteps[0].missionType;
        bool sameStationType = mission.missionData.missionSteps[0].stationType == checkMission.missionData.missionSteps[0].stationType;

        return sameMisionType && sameStationType;
    }

    private void CheckMissionsCompleted()
    {
        foreach (Mission mission in currentMissions)
        {
            if (mission.CheckMissionCompleted())
            {
                research.AddResearchPoints(mission.missionReward * missionRewardMultiplyer);
                completedMissions.Add(mission);
                onMissionCompleted?.Invoke();
            }
        }

        foreach (Mission mission in completedMissions)
        {
            if (currentMissions.Contains(mission))
            {
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
