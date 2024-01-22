using System.Collections.Generic;
using UnityEngine;

public class InsertItemTask : Task
{
    [SerializeField] List<ItemData> possibleItems;

    public override void StartTask(Interactable interactable)
    {
        base.StartTask(interactable);

        ItemData playerItem = FindObjectOfType<PlayerInventory>().GetCurrentItem();

        if (possibleItems.Contains(playerItem) || playerItem == null)
        {
            interactable.ExchangeItems();
            interactable.CompleteTask();
        }

        EndTask();
    }
}