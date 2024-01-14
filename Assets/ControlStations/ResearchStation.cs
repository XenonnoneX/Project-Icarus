using System;
using UnityEngine;

public class ResearchStation : ControlStation
{
    SpaceShipMovement spaceShipMovement;

    [SerializeField] float RPStorageCapacity = 100;
    [HideInInspector] public float addedRPStorageCapacity = 0;

    float savedRP;
    float onBoardStoredRP;

    float papersReleased;

    public int GetSavedRP()
    {
        return (int) savedRP;
    }

    public int PapersReleased() => (int)papersReleased;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (stationState != StationState.Working) return;

        CheckReleasePaper(); 
        
        AddResearchPoints(ResearchPointsPerSecond() * Time.deltaTime * timeScale);
    }

    private float ResearchPointsPerSecond()
    {
        int shipHeight = spaceShipMovement.GetCurrentHeight() + 1; // to not divide by 0

        return 10f / shipHeight;
    }

    private void CheckReleasePaper()
    {
        float rand = UnityEngine.Random.Range(0f, 1f);

        // the higher the savedRP Count the higher probability to release a paper

        papersReleased += rand * savedRP / 100 * Time.deltaTime;

        savedRP = Mathf.Max(0, savedRP - Time.deltaTime);
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
