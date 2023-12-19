using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
            if (missionData.missionSteps[i].Completed()) stepsCompleted[i] = true;
            else allStepsCompleted = false;
        }

        return allStepsCompleted;
    }
}
