using UnityEngine;

public class AnomalyAnalizerLight : MonoBehaviour
{
    AnomalyAnalysisStation anomalyAnalysisStation;

    [SerializeField] Light2D analizerLight;

    private void Awake()
    {
        anomalyAnalysisStation = FindObjectOfType<AnomalyAnalysisStation>();
        if (analizerLight == null)
        {
            analizerLight = GetComponent<Light2D>();
        }
    }

    private void Update()
    {
        if (anomalyAnalysisStation.GetStationState() != StationState.Working)
        {
            analizerLight.SetAlphaMulitplier(0);
        }
        else if (anomalyAnalysisStation.AnalysisCompleted())
        {
            analizerLight.SetAlphaMulitplier(1);
            analizerLight.SetBlinkSpeedMultiplier(0f);
        }
        else if (anomalyAnalysisStation.CurrentlyAnalizing())
        {
            analizerLight.SetAlphaMulitplier(0.75f);
            analizerLight.SetBlinkSpeedMultiplier(5f);
        }
        else
        {
            analizerLight.SetAlphaMulitplier(0);
        }
    }
}
