using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    ReachHeightBelow,
    CompleteStationTask,
    GetHitByTinyBH,
    DropItemOutInSpace,
    FlyFarAway,
    RepairInSpeedField,
    RepairInSlowField,
    LensDuringHIAnomaly
}

[CreateAssetMenu()]
public class MissionData : ScriptableObject
{
    public List<MissionStep> missionSteps;
    
    public string missionText;
    public Sprite missionTextImage;
    public int missionReward = 50;

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

    [ConditionalHide("missionType", (int)MissionType.LensDuringHIAnomaly)]
    public ItemData neededLens;


    [HideInInspector] public ControlStation station;
    bool missionStepCompleted;
    

    internal bool Completed()
    {
        if(missionType == MissionType.ReachHeightBelow)
        {
            return ReachedHeightBelow(height);
        }
        else if (
            missionType == MissionType.CompleteStationTask || 
            missionType == MissionType.GetHitByTinyBH ||
            missionType == MissionType.DropItemOutInSpace ||
            missionType == MissionType.FlyFarAway ||
            missionType == MissionType.RepairInSlowField || 
            missionType == MissionType.RepairInSpeedField ||
            missionType == MissionType.LensDuringHIAnomaly)
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
        else if (missionType == MissionType.GetHitByTinyBH)
        {
            PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            playerMovement.onHitByBH += CheckCompleteMissionStep;

            if (missionStepCompleted)
            {
                missionStepCompleted = false;
                // not sure why this is needed, but when walking into a BH before the mission is active, the mission is completed already at setup
            }
        }
        else if (missionType == MissionType.DropItemOutInSpace)
        {
            PlayerInventory playerInventory = GameObject.FindObjectOfType<PlayerInventory>();
            playerInventory.onDropedItem += CheckDropedItemInSpace;
        }
        else if (missionType == MissionType.FlyFarAway)
        {
            PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            playerMovement.OnLoopBoundaries += CheckCompleteMissionStep;
        }else if(missionType == MissionType.RepairInSlowField)
        {
            RepairTask repairTask = GameObject.FindObjectOfType<RepairTask>();
            repairTask.onStartTask += CheckRepairInSlowField;
        }else if(missionType == MissionType.RepairInSpeedField)
        {
            RepairTask repairTask = GameObject.FindObjectOfType<RepairTask>();
            repairTask.onStartTask += CheckRepairInSpeedField;
        }else if(missionType == MissionType.LensDuringHIAnomaly)
        {
            HazardManager hazardManager = GameObject.FindObjectOfType<HazardManager>();
            hazardManager.OnSpawnHIAnomaly += CheckCorrectLensInTelescope;
            GameObject.FindObjectOfType<Telescope>().onLensChanged += CheckHIHazardActiveAndCorrectLens;
        }
    }

    private void CheckHIHazardActiveAndCorrectLens(ItemData lens)
    {
        if (lens != neededLens) return;

        if (GameObject.FindObjectOfType<HazardManager>().HighImpactAnomalyActive) missionStepCompleted = true;
    }

    private void CheckCorrectLensInTelescope()
    {
        Telescope telescope = GameObject.FindObjectOfType<Telescope>();

        if (telescope.GetCurrentLens() == neededLens) missionStepCompleted = true;
    }

    private void CheckRepairInSlowField(float timeScale)
    {
        Debug.Log("CheckRepeir slow: " + timeScale);
        if (timeScale < 1) missionStepCompleted = true;
    }

    private void CheckRepairInSpeedField(float timeScale)
    {
        Debug.Log("CheckRepeir fast: " + timeScale);
        if (timeScale > 1) missionStepCompleted = true;
    }

    private void CheckDropedItemInSpace(bool outOfShip)
    {
        if (outOfShip)
        {
            missionStepCompleted = true;
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
                    SetMissionStepCompleted(true);
                }
            }
            else
            {
                SetMissionStepCompleted(true);
            }
        }
        else if (missionType == MissionType.GetHitByTinyBH)
        {
            SetMissionStepCompleted(true);
        }
        else if (missionType == MissionType.FlyFarAway)
        {
            SetMissionStepCompleted(true);
        }
    }

    void SetMissionStepCompleted(bool value)
    {
        missionStepCompleted = value;
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
