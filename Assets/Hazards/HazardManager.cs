using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HazardType
{
    BreakStation,
    SpawnAlien,
    SpawnAnomaly
}

public class HazardManager : MonoBehaviour
{
    SpaceShipMovement spaceShipMovement;
    List<ControlStation> allControlStations = new List<ControlStation>();

    Spawner<Alien> alienSpawner;
    Spawner<Anomaly> anomalySpawner;

    [SerializeField] List<Alien> allAliens;
    [SerializeField] List<Anomaly> allAnomalies;

    [SerializeField] float timeToGetNewHazard = 10f;
    float newHazardTimer;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
        allControlStations.AddRange(FindObjectsOfType<ControlStation>());
    }

    private void Start()
    {
        alienSpawner = new Spawner<Alien>();
        alienSpawner.allObjects.AddRange(allAliens);

        anomalySpawner = new Spawner<Anomaly>();
        anomalySpawner.allObjects.AddRange(allAnomalies);
    }

    private void Update()
    {
        newHazardTimer += Time.deltaTime;
        
        if(newHazardTimer > TimeToGetNewHazard())
        {
            newHazardTimer = 0;
            CauseRandomHazard();
        }
    }

    private float TimeToGetNewHazard()
    {
        return timeToGetNewHazard * (spaceShipMovement.GetCurrentHeight() + 1);
    }

    private void CauseRandomHazard()
    {
        int hazardTypeCount = Enum.GetValues(typeof(HazardType)).Length;
        HazardType randomHazard = (HazardType)UnityEngine.Random.Range(0, hazardTypeCount);

        switch (randomHazard)
        {
            case HazardType.BreakStation:
                BreakRandomStation();
                break;
            case HazardType.SpawnAlien:
                SpawnAlien();
                break;
            case HazardType.SpawnAnomaly:
                SpawnAnomaly();
                break;
        }
    }

    private void BreakRandomStation()
    {
        print("Break Station");
        allControlStations[UnityEngine.Random.Range(0, allControlStations.Count)].SetIsBroken(true);
    }

    private void SpawnAlien()
    {
        print("Spawn Alien");
        alienSpawner.SpawnRandom();
    }

    private void SpawnAnomaly()
    {
        print("Spawn Anomaly");
        anomalySpawner.SpawnRandom();
    }
}
