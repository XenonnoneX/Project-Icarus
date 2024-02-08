using System;
using System.Collections;
using UnityEngine;

public class AnomalyScannerVisuals : MonoBehaviour
{
    AnomalyScanner anomalyScanner;
    ShowInventoryItem inventoryShow;
    SpriteRenderer spriteRenderer;
    LineRenderer lineRenderer;

    [SerializeField] float animationTime = 0.3f;

    private void Awake()
    {
        anomalyScanner = GetComponent<AnomalyScanner>();
        inventoryShow = FindObjectOfType<ShowInventoryItem>();

        spriteRenderer = inventoryShow.GetItemSpriteRenderer();
        lineRenderer = GetComponent<LineRenderer>();

        anomalyScanner.onAnomalyChanged += SetAnomalyScannerVisuals;
        anomalyScanner.onSuckup += SuckupEffect;
    }

    private void Start()
    {
        lineRenderer.positionCount = 2;

        lineRenderer.enabled = false;
    }

    private void SuckupEffect(Anomaly anomaly)
    {
        StartCoroutine(SuckupRoutine(anomaly.transform.position));
    }

    void SetAnomalyScannerVisuals(AnomalyType type)
    {
        if (type == AnomalyType.None) spriteRenderer.color = Color.white;
        else spriteRenderer.color = Color.red;
    }

    IEnumerator SuckupRoutine(Vector3 anomalyPosition)
    {
        lineRenderer.enabled = true;

        for (float i = 0; i < animationTime; i+= Time.deltaTime)
        {
            Vector3[] positions = new Vector3[] { Vector3.Lerp(anomalyPosition, anomalyScanner.transform.position, i / animationTime), Vector3.Lerp(anomalyPosition, anomalyScanner.transform.position, Mathf.Clamp01(2 * i / animationTime)) };
            
            lineRenderer.SetPositions(positions);
            yield return null;
        }

        lineRenderer.enabled = false;
    }
}