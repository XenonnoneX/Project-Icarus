using System;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    ReachHeightBelow,
    CompleteStationTask,
    GetHitByTineBH
}

[CreateAssetMenu()]
public class MissionData : ScriptableObject
{
    public List<MissionStep> missionSteps;
    
    public string missionText;
    public int missionReward;

    internal void Setup()
    {
        foreach (MissionStep step in missionSteps)
        {
            step.Setup();
        }
    }
}

[System.Serializable]
public class MissionStep
{
    public MissionType missionType;

    [ConditionalHide("missionType", (int)MissionType.ReachHeightBelow)]
    public int height;
    
    [ConditionalHide("missionType", (int)MissionType.CompleteStationTask)]
    public StationType stationType;

    [ConditionalHide("stationType", (int)StationType.AnomalyAnalizer)]
    public AnomalyType anomalyType;


    [HideInInspector] public ControlStation station;
    bool missionStepCompleted;
    

    internal bool Completed()
    {
        if(missionType == MissionType.ReachHeightBelow)
        {
            return ReachedHeightBelow(height);
        }else if (missionType == MissionType.CompleteStationTask || missionType == MissionType.GetHitByTineBH)
        {
            bool completed = missionStepCompleted;
            missionStepCompleted = false;
            
            return completed;
        }
        else
        {
            Debug.Log("Mission type not implemented");
            return true;
        }
    }

    public void Setup()
    {
        if (missionType == MissionType.CompleteStationTask)
        {
            station = GetStationOfType(stationType);
            station.onCompleteTask += CheckCompleteMissionStep;
        }
        else if (missionType == MissionType.GetHitByTineBH)
        {
            PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            playerMovement.onHitByBH += CheckCompleteMissionStep;
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

    void CheckCompleteMissionStep()
    {
        if (missionType == MissionType.CompleteStationTask)
        {
            if (stationType == StationType.AnomalyAnalizer)
            {
                if (((AnomalyAnalysisStation)station).CurrentAnomalyType == anomalyType)
                {
                    missionStepCompleted = true;
                }
            }
            else
            {
                missionStepCompleted = true;
            }
        }
        else if (missionType == MissionType.GetHitByTineBH)
        {
            missionStepCompleted = true;
        }
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
