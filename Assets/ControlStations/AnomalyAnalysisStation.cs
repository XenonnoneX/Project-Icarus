using UnityEngine;

public class AnomalyAnalysisStation : ControlStation
{
    AnomalyScanner anomalyScanner;

    AnomalyType currentAnomalyType;

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

        timeSinceAnomalyAnalysisStarted += Time.deltaTime * timeScale;
    }

    public void SetAnomalyType(AnomalyType anomalyType)
    {
        currentAnomalyType = anomalyType;
        timeSinceAnomalyAnalysisStarted = 0;
    }

    public override void CompleteTask()
    {
        if (currentAnomalyType == AnomalyType.None)
        {
            currentAnomalyType = anomalyScanner.GetCapturedAnomalyType;
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
    }
}