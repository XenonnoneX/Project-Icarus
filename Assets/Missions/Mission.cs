using System;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public MissionData missionData;
    public List<bool> stepsCompleted = new List<bool>();
    
    public string missionText;
    public int missionReward;

    public Mission(MissionData missionData)
    {
        this.missionData = missionData;

        for (int i = 0; i < missionData.missionSteps.Count; i++)
        {
            stepsCompleted.Add(false);
        }
        
        missionText = missionData.missionText;
        missionReward = missionData.missionReward;
    }

    public bool CheckMissionCompleted()
    {
        bool allStepsCompleted = true;
        for (int i = 0; i < missionData.missionSteps.Count; i++)
        {
            if (missionData.missionSteps[i].Completed())
            {
                // Debug.Log("Mission Step Completed: " + missionData.missionSteps[i].missionType);
                // Debug.Log("Total Step count: " + missionData.missionSteps.Count);
                stepsCompleted[i] = true;
            }
            else allStepsCompleted = false;
        }

        return allStepsCompleted;
    }

    internal void Setup()
    {
        missionData.Setup();
    }
}
