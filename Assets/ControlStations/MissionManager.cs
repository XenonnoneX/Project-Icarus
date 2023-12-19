using System.Collections.Generic;
using UnityEngine;

public class MissionManager : ControlStation
{
    Research research;

    public int maxMissions = 3;
    [SerializeField] float timeToGetNewMission = 10f;
    float newMissionTimer;

    [SerializeField] List<MissionData> allMissions;
    public List<Mission> currentMissions = new List<Mission>();
    [SerializeField] List<Mission> completedMissions = new List<Mission>();

    public delegate void OnMissionsChanged();
    public OnMissionsChanged onMissionsChanged;

    private void Awake()
    {
        research = FindObjectOfType<Research>();
    }

    private void Update()
    {
        CheckMissionsCompleted();

        if (isBroken) return;

        GetNewMissions();
    }

    private void GetNewMissions()
    {
        newMissionTimer += Time.deltaTime;

        if (newMissionTimer > timeToGetNewMission && currentMissions.Count < maxMissions && currentMissions.Count < allMissions.Count)
        {
            newMissionTimer = 0;

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

        print("New Random Mission: " + rand);

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
            if (mission.CheckMissionCompleted())
            {
                research.AddResearchPoints(mission.missionReward);
                completedMissions.Add(mission);
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
}
