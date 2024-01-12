using UnityEngine;

public class AnomalyScannerVisuals : MonoBehaviour
{
    AnomalyScanner anomalyScanner;
    ShowInventoryItem inventoryShow;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anomalyScanner = GetComponent<AnomalyScanner>();
        inventoryShow = FindObjectOfType<ShowInventoryItem>();

        spriteRenderer = inventoryShow.GetItemSpriteRenderer();

        anomalyScanner.onAnomalyChanged += SetAnomalyScannerVisuals;
    }

    void SetAnomalyScannerVisuals(AnomalyType type)
    {
        if (type == AnomalyType.None) spriteRenderer.color = Color.white;
        else spriteRenderer.color = Color.red;
    }
}