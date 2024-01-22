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
    float breakingStationTimer;
    float timeSinceStart;
    float newHighImpactAnomalyTimer;

    bool highImpactAnomalyActive;

    public delegate void OnHighImpactAnomalyWarning(Anomaly anomaly);
    public OnHighImpactAnomalyWarning onHighImpactAnomalyWarning;

    
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
        while (!allControlStations[rand].CanBreak() && iterations < maxIterations);
        
        if(interactableDetector.CurrentInteractingInteractable != null && ((Interactable)interactableDetector.CurrentInteractingInteractable).station == allControlStations[rand])
        {
            interactableDetector.CurrentInteractingInteractable.CancelTask();
        }

        allControlStations[UnityEngine.Random.Range(0, allControlStations.Count)].SetStationState(StationState.Broken);
    }

    private void SpawnAnomaly()
    {
        Anomaly temp = GameObject.Instantiate(allAnomalies[UnityEngine.Random.Range(0, allAnomalies.Count)], Utils.GetRandomPosOnWalkableArea(), Quaternion.identity);
        temp.transform.parent = hazardParent;
    }
    
    System.Collections.IEnumerator SpawnHighImpactAnomaly()
    {
        Anomaly anomaly = highImpactAnomalies[UnityEngine.Random.Range(0, highImpactAnomalies.Count)];
        onHighImpactAnomalyWarning.Invoke(anomaly);

        yield return new WaitForSeconds(highImpactAnomalyWarningTime);

        highImpactAnomalyActive = true;

        Anomaly temp = GameObject.Instantiate(anomaly, Utils.GetRandomPosOnWalkableArea(), Quaternion.identity);

        yield return new WaitForSeconds(timeFreeOfOtherHazardsAfterHighImpactAnomaly);

        highImpactAnomalyActive = false;
    }
}
