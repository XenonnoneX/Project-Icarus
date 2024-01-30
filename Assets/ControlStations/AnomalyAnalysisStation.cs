using System;
using UnityEngine;

public class AnomalyAnalysisStation : ControlStation
{
    AnomalyScanner anomalyScanner;

    AnomalyType currentAnomalyType;
    public AnomalyType CurrentAnomalyType => currentAnomalyType;

    [SerializeField] float anomalyAnalysisTime = 15f;
    float timeSinceAnomalyAnalysisStarted = 0;

    private void Awake()
    {
        anomalyScanner = FindObjectOfType<AnomalyScanner>();
    }

    protected override void Update()
    {
        base.Update();

        if (stationState != StationState.Working) return;

        if (currentAnomalyType == AnomalyType.None) return;
        
        timeSinceAnomalyAnalysisStarted += Time.deltaTime * timeScale;
    }

    public void SetAnomalyType(AnomalyType anomalyType)
    {
        currentAnomalyType = anomalyType;
        timeSinceAnomalyAnalysisStarted = 0;
    }

    public override void CompleteTask(ItemData currentItem = null)
    {
        if (timeSinceAnomalyAnalysisStarted == 0)
        {
            currentAnomalyType = anomalyScanner.GetCapturedAnomalyType;
            anomalyScanner.SetAnomalyType(AnomalyType.None);
            // anomalyScanner.SetAnomalyType(currentAnomalyType); // if other anomaly was inside swap them
        }
        else if (timeSinceAnomalyAnalysisStarted > anomalyAnalysisTime)
        {
            onCompleteTask?.Invoke();
            print("Task completed");
            FinishAnalysis();
            SetAnomalyType(AnomalyType.None);
        }
    }

    private void FinishAnalysis()
    {
        print("Analysis of " + currentAnomalyType + " finished");
        timeSinceAnomalyAnalysisStarted = 0;
    }

    internal bool AnalysisCompleted() => timeSinceAnomalyAnalysisStarted >= anomalyAnalysisTime;

    internal bool CurrentlyAnalizing() => timeSinceAnomalyAnalysisStarted > 0 && timeSinceAnomalyAnalysisStarted < anomalyAnalysisTime;

    internal float GetAnomalyAnalysisProgress()
    {
        return timeSinceAnomalyAnalysisStarted / anomalyAnalysisTime;
    }
}