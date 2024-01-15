using System;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyScanner : MonoBehaviour
{
    PlayerInventory inventory;
    [SerializeField] ItemData anomalyScannerItem;

    [SerializeField] Transform hazardParent;
    List<Anomaly> anomaliesInRange;
    [SerializeField] float scannRange = 3f;

    AnomalyType capturedAnomalyType;
    public AnomalyType GetCapturedAnomalyType => capturedAnomalyType;

    public delegate void OnAnomalyChanged(AnomalyType anomalyType);
    public event OnAnomalyChanged onAnomalyChanged;

    private void Awake()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }

    private void Start()
    {
        InvokeRepeating("GetAnomaliesInRange", 0, 0.5f);
    }

    void OnInteract()
    {
        if (inventory.GetCurrentItem() != anomalyScannerItem) return;

        if (anomaliesInRange.Count == 0) return;

        if (capturedAnomalyType == AnomalyType.None)
        {
            Anomaly closestAnomaly = GetClosestAnomaly();

            if (closestAnomaly == null) return;

            SetAnomalyType(closestAnomaly.anomalyType);

            closestAnomaly.RemoveAnomaly();
        }
    }

    Anomaly GetClosestAnomaly()
    {
        float closestDistance = float.MaxValue;

        Anomaly closestAnomaly = null;

        foreach (Anomaly anomaly in GetAnomaliesInRange())
        {
            float distance = Vector2.Distance(transform.position, anomaly.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestAnomaly = anomaly;
            }
        }

        return closestAnomaly;
    }

    private IEnumerable<Anomaly> GetAnomaliesInRange()
    {
        anomaliesInRange = new List<Anomaly>();

        if (inventory.GetCurrentItem() != anomalyScannerItem) return anomaliesInRange;

        Anomaly[] allAnomalies = hazardParent.GetComponentsInChildren<Anomaly>();

        foreach (Anomaly anomaly in allAnomalies)
        {
            if (Vector3.Distance(transform.position, anomaly.transform.position) < scannRange) anomaliesInRange.Add(anomaly);
        }

        return anomaliesInRange;
    }
    
    internal void SetAnomalyType(AnomalyType type)
    {
        capturedAnomalyType = type;
        onAnomalyChanged?.Invoke(capturedAnomalyType);
    }
}
