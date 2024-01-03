using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ResearchStation : ControlStation
{
    SpaceShipMovement spaceShipMovement;

    [SerializeField] float RPStorageCapacity = 100;
    [HideInInspector] public float addedRPStorageCapacity = 0;

    float savedRP;
    float onBoardStoredRP;

    public int GetSavedRP()
    {
        return (int) savedRP;
    }

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (stationState != StationState.Working) return;

        AddResearchPoints(ResearchPointsPerSecond() * Time.deltaTime * timeScale);
    }

    private float ResearchPointsPerSecond()
    {
        int shipHeight = spaceShipMovement.GetCurrentHeight() + 1; // to not divide by 0

        return 10f / shipHeight;
    }

    public void AddResearchPoints(float researchPoints)
    {
        onBoardStoredRP += researchPoints;

        if (onBoardStoredRP > RPStorageCapacity + addedRPStorageCapacity) onBoardStoredRP = RPStorageCapacity + addedRPStorageCapacity;
    }

    public float GetRPStorageFillPercentage()
    {
        return onBoardStoredRP / (RPStorageCapacity + addedRPStorageCapacity);
    }

    public override void CompleteTask()
    {
        savedRP += onBoardStoredRP;
        onBoardStoredRP = 0;

        base.CompleteTask();
    }
}
