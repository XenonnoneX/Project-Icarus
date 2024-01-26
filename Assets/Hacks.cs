using UnityEngine;

public class Hacks : MonoBehaviour
{
    PlayerMovement playerMovement;
    AnomalyScanner anomalyScanner;

    [SerializeField] ItemData repairKit;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        anomalyScanner = FindObjectOfType<AnomalyScanner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) playerMovement.GetHitByBH();

        if (Input.GetKeyDown(KeyCode.L)) anomalyScanner.SetAnomalyType(AnomalyType.None);

        if (Input.GetKeyDown(KeyCode.P)) FindObjectOfType<PlayerInventory>().PickUpItem(repairKit);

        if (Input.GetKeyDown(KeyCode.I)) FindObjectOfType<HazardManager>().HackBreakRandomStation();
    }
}