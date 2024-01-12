using System;
using System.Collections.Generic;
using UnityEngine;


public enum HazardType
{
    BreakStation,
    SpawnAnomaly
}

public class HazardManager : MonoBehaviour
{
    SpaceShipMovement spaceShipMovement;
    InteractableDetector interactableDetector;
    List<ControlStation> allControlStations = new List<ControlStation>();
    
    Spawner<Anomaly> anomalySpawner;
    
    [SerializeField] List<Anomaly> allAnomalies;
    [SerializeField] Transform hazardParent;

    [SerializeField] float timeToGetNewHazard = 2f;
    [SerializeField] float timeBetweenHazardsDecreasePerMinute = 0.9f;
    [SerializeField] float timeBetweenBreakingStations = 1.5f;
    [SerializeField] float timeBetweenBreakingStationsDecreasePerMinute = 0.9f;
    float newHazardTimer;
    float breakingStationTimer;
    float timeSinceStart;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
        interactableDetector = FindObjectOfType<InteractableDetector>();
        allControlStations.AddRange(FindObjectsOfType<ControlStation>());
    }

    private void Start()
    {
        anomalySpawner = new Spawner<Anomaly>();
        anomalySpawner.allObjects.AddRange(allAnomalies);
    }

    private void Update()
    {
        newHazardTimer += Time.deltaTime;
        breakingStationTimer += Time.deltaTime;
        timeSinceStart += Time.deltaTime;

        if (newHazardTimer > TimeToGetNewHazard())
        {
            newHazardTimer = 0;
            SpawnAnomaly();
        } else if(breakingStationTimer > TimeToBreakStation())
        {
            breakingStationTimer = 0;
            BreakRandomStation();
        }
    }

    private float TimeToGetNewHazard()
    {
        return (timeToGetNewHazard * (Mathf.Pow(timeBetweenHazardsDecreasePerMinute,timeSinceStart / 60))) * (spaceShipMovement.GetCurrentHeight() + 1);
    }

    private float TimeToBreakStation()
    {
        return (timeBetweenBreakingStations * (Mathf.Pow(timeBetweenBreakingStationsDecreasePerMinute, timeSinceStart / 60))) * (spaceShipMovement.GetCurrentHeight() + 1);
    }

    private void BreakRandomStation()
    {
        print("beaksksdj");

        int rand = UnityEngine.Random.Range(0, allControlStations.Count);

        if (!allControlStations[rand].CanBreak())
        {
            print("cant break");
            return;
        }
        
        if(interactableDetector.CurrentInteractingInteractable != null && ((Interactable)interactableDetector.CurrentInteractingInteractable).station == allControlStations[rand])
        {
            interactableDetector.CurrentInteractingInteractable.CancelTask();
        }

        allControlStations[UnityEngine.Random.Range(0, allControlStations.Count)].SetStationState(StationState.Broken);
    }

    private void SpawnAnomaly()
    {
        anomalySpawner.SpawnRandom(hazardParent);
    }
}
