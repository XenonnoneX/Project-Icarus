using System.Collections;
using TMPro;
using UnityEngine;

public class HighImpactAnomalyWarnings : MonoBehaviour
{
    HazardManager hazardManager;

    [SerializeField] GameObject showWarning;
    [SerializeField] TMP_Text warningText;

    private void Awake()
    {
        hazardManager = FindObjectOfType<HazardManager>();

        hazardManager.onHighImpactAnomalyWarning += ShowWarning;
    }

    private void Start()
    {
        HideWarning();
    }

    private void ShowWarning(Anomaly anomaly)
    {
        showWarning.SetActive(true);

        StartCoroutine(ShowWarningCooldown(anomaly));
    }

    IEnumerator ShowWarningCooldown(Anomaly anomaly)
    {
        for (int i = 0; i < hazardManager.HighImpactAnomalyWarningTime; i++)
        {
            warningText.text = $"High impact anomaly detected. Type: {anomaly.anomalyType}.\nImpact in {hazardManager.HighImpactAnomalyWarningTime - i} seconds.";
            yield return new WaitForSeconds(1f);
        }

        warningText.text = $"High impact anomaly detected. Type: {anomaly.anomalyType}.\nImpact now!";

        yield return new WaitForSeconds(anomaly.AnomalyDuration);

        HideWarning();
    }

    private void HideWarning()
    {
        showWarning.SetActive(false);
    }
}