using UnityEngine;

public class RepairKitArtifact : Artifact
{
    PlayerInventory inventory;
    [SerializeField] ItemData repairKit;

    protected override void EnableArtifact()
    {
        base.EnableArtifact();

        if (inventory == null) inventory = FindObjectOfType<PlayerInventory>();
        inventory.PickUpItem(repairKit);

        currentLevel = 0;
    }
}