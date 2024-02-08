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
    MissionManager missionManager;
    InteractableDetector interactableDetector;
    List<ControlStation> allControlStations = new List<ControlStation>();
    
    Spawner<Anomaly> anomalySpawner;
    
    [SerializeField] List<Anomaly> allAnomalies;
    [SerializeField] List<Anomaly> highImpactAnomalies;
    [SerializeField] Transform hazardParent;

    [SerializeField] float timeBetweenAnomaliesAtHeight0 = 2f;
    [SerializeField] float timeBetweenAnomaliesDecreasePerMinute = 0.9f;
    [SerializeField] float timeBetweenBreakingStationsAtHeight0 = 1.5f;
    [SerializeField] float timeBetweenBreakingStationsDecreasePerMinute = 0.9f;
    [SerializeField] float timeBetweenHighImpactAnomalies = 60;
    [SerializeField] float highImpactAnomalyWarningTime = 10f;
    [SerializeField] float timeFreeOfOtherHazardsAfterHighImpactAnomaly = 10f;
    public float HighImpactAnomalyWarningTime => highImpactAnomalyWarningTime;
    float newAnomalyTimer;
    int lastSpawnedAnomalyIndex;
    float breakingStationTimer;
    float timeSinceStart;
    float newHighImpactAnomalyTimer;

    bool highImpactAnomalyActive;
    public bool HighImpactAnomalyActive => highImpactAnomalyActive;

    public delegate void OnHighImpactAnomalyWarning(Anomaly anomaly);
    public OnHighImpactAnomalyWarning onHighImpactAnomalyWarning;

    public event Action OnSpawnHIAnomaly;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
        missionManager = FindObjectOfType<MissionManager>();
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
        if (!highImpactAnomalyActive)
        {
            newAnomalyTimer += Time.deltaTime;
            breakingStationTimer += Time.deltaTime;
            timeSinceStart += Time.deltaTime;
            newHighImpactAnomalyTimer += Time.deltaTime;
        }

        if (newAnomalyTimer > TimeToGetNewHazard())
        {
            newAnomalyTimer = 0;
            SpawnAnomaly();
        } else if(breakingStationTimer > TimeToBreakStation())
        {
            breakingStationTimer = 0;
            BreakRandomStation();
        }
        else if (newHighImpactAnomalyTimer > timeBetweenHighImpactAnomalies)
        {
            newHighImpactAnomalyTimer = 0;
            StartCoroutine(SpawnHighImpactAnomaly());
        }
    }

    private float TimeToGetNewHazard()
    {
        return (timeBetweenAnomaliesAtHeight0 * (Mathf.Pow(timeBetweenAnomaliesDecreasePerMinute,timeSinceStart / 60))) * (spaceShipMovement.GetCurrentHeight() + 1);
    }

    private float TimeToBreakStation()
    {
        return (timeBetweenBreakingStationsAtHeight0 * (Mathf.Pow(timeBetweenBreakingStationsDecreasePerMinute, timeSinceStart / 60))) * (spaceShipMovement.GetCurrentHeight() + 1);
    }

    public void HackBreakRandomStation()
    {
        BreakRandomStation();
    }

    private void BreakRandomStation()
    {
        int rand;

        int iterations = 0;
        int maxIterations = 20;
        
        do
        {
            rand = UnityEngine.Random.Range(0, allControlStations.Count);
            iterations++;
        }
        while ((!allControlStations[rand].CanBreak() || allControlStations[rand].GetStationState() == StationState.Destroyed) && iterations < maxIterations);

        if (iterations < maxIterations)
        {
            if (interactableDetector.CurrentInteractingInteractable != null && ((Interactable)interactableDetector.CurrentInteractingInteractable).station == allControlStations[rand])
            {
                interactableDetector.CurrentInteractingInteractable.CancelTask();
            }
            
            allControlStations[rand].SetStationState(StationState.Broken);
        }
    }

    private void SpawnAnomaly()
    {
        int index = GetIndexOfAnomalyNeededForMission();
        while (index == lastSpawnedAnomalyIndex)
        {
            index = UnityEngine.Random.Range(0, anomalySpawner.allObjects.Count);
        }

        lastSpawnedAnomalyIndex = index;

        Anomaly temp = GameObject.Instantiate(allAnomalies[index], Utils.GetRandomPosOnWalkableArea(), Quaternion.identity);
        temp.transform.parent = hazardParent;
    }

    private int GetIndexOfAnomalyNeededForMission()
    {
        foreach (Mission mission in missionManager.currentMissions)
        {
            if (mission.missionData.missionSteps[0].anomalyType == AnomalyType.None)
            {
                continue;
            }

            for (int i = 0; i < anomalySpawner.allObjects.Count; i++)
            {
                if (anomalySpawner.allObjects[i].anomalyType == mission.missionData.missionSteps[0].anomalyType)
                {
                    if (i == lastSpawnedAnomalyIndex) continue;
                    else return i;
                }
            }
        }

        return lastSpawnedAnomalyIndex;
    }

    System.Collections.IEnumerator SpawnHighImpactAnomaly()
    {
        Anomaly anomaly = highImpactAnomalies[UnityEngine.Random.Range(0, highImpactAnomalies.Count)];
        onHighImpactAnomalyWarning.Invoke(anomaly);

        yield return new WaitForSeconds(highImpactAnomalyWarningTime);

        highImpactAnomalyActive = true;

        Anomaly temp = GameObject.Instantiate(anomaly, Utils.GetRandomPosOnWalkableArea(), Quaternion.identity);

        OnSpawnHIAnomaly?.Invoke();

        yield return new WaitForSeconds(timeFreeOfOtherHazardsAfterHighImpactAnomaly);

        // yield return new WaitForSeconds(temp.AnomalyDuration - timeFreeOfOtherHazardsAfterHighImpactAnomaly); // player can only fulfill the telescope mission before or in the first few seconds of the anomaly

        highImpactAnomalyActive = false;
    }
}
