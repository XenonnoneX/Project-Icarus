using UnityEngine;

public class Door : ControlStation
{
    PlayerMovement playerMovement;

    [SerializeField] Transform insideSpawnPoint;
    [SerializeField] Transform outsideSpawnPoint;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public override void CompleteTask(ItemData currentItem = null)
    {
        base.CompleteTask(currentItem);

        if (Utils.OutOfShip(playerMovement.transform)) playerMovement.transform.position = insideSpawnPoint.position;
        else playerMovement.transform.position = outsideSpawnPoint.position;
    }
}