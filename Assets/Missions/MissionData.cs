using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MissionType
{
    ReachHeightBelow,
    CompleteStationTask
}

[CreateAssetMenu()]
public class MissionData : ScriptableObject
{
    public List<MissionStep> missionSteps;
    
    public string missionText;
    public int missionReward;
}

[System.Serializable]
public class MissionStep
{
    public MissionType missionType;

    [ConditionalHide("missionType", (int)MissionType.ReachHeightBelow)]
    public int height;
    
    [ConditionalHide("missionType", (int)MissionType.CompleteStationTask)]
    public StationType stationType;
    [ConditionalHide("missionType", (int)MissionType.CompleteStationTask)]
    [HideInInspector] public ControlStation station;
    bool stationTaskCompleted;

    [SerializeField] bool setupDone = false;

    internal bool Completed()
    {
        Setup();

        if(missionType == MissionType.ReachHeightBelow)
        {
            if (ReachedHeightBelow(height)) Debug.Log("Reached Height completed task");
            return ReachedHeightBelow(height);
        }else if (missionType == MissionType.CompleteStationTask)
        {
            bool completed = stationTaskCompleted;
            stationTaskCompleted = false;

            if (completed) Debug.Log("Station Task Completed");
            
            return completed;
        }
        else
        {
            Debug.Log("Mission type not implemented");
            return true;
        }
    }

    private void Setup()
    {
        Debug.Log("SetupDone: " + setupDone);

        if (setupDone) return;

        Debug.Log("Setup not done");

        setupDone = true;

        if (missionType == MissionType.CompleteStationTask)
        {
            Debug.Log("Subscribing to station");
            station = GetStationOfType(stationType);
            station.onCompleteTask += OnCompleteStationTask;
        }
    }

    private bool ReachedHeightBelow(int height)
    {
        if (SpaceShipMovement.instance.GetCurrentHeight() < height)
        {
            return true;
        }
        return false;
    }

    void OnCompleteStationTask()
    {
        Debug.Log("OnCOmpleteStationTask");
        stationTaskCompleted = true;
    }



    internal ControlStation GetStationOfType(StationType stationType)
    {
        foreach (ControlStation station in StationManager.instance.controlStations)
        {
            if (station.stationType == stationType)
            {
                return station;
            }
        }
        return null;
    }
}
