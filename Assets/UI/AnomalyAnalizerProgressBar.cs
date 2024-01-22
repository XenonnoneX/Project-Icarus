using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyAnalizerProgressBar : MonoBehaviour
{
    AnomalyAnalysisStation anomalyAnalysisStation;

    [SerializeField] Image image;
    
    private void Awake()
    {
        anomalyAnalysisStation = FindObjectOfType<AnomalyAnalysisStation>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = anomalyAnalysisStation.GetAnomalyAnalysisProgress();
    }
}
