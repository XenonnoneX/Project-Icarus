using UnityEngine;

public class Hacks : MonoBehaviour
{
    PlayerMovement playerMovement;
    AnomalyScanner anomalyScanner;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        anomalyScanner = FindObjectOfType<AnomalyScanner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) playerMovement.GetHitByBH();

        if (Input.GetKeyDown(KeyCode.L)) anomalyScanner.SetAnomalyType(AnomalyType.None);
    }
}