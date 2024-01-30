using System;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static UnityEditor.Progress;

public class Interactable : MonoBehaviour, IInteractable, TimeAffected
{
    PlayerInventory playerInventory;
    RepairTask repairTask;
    
    public ControlStation station;
    [SerializeField] Task task;
    [SerializeField] ItemData neededItem;

    [SerializeField] GameObject showInteractable;
    [SerializeField] GameObject showBroken;
    [SerializeField] GameObject showDestroyed;

    public Transform myTransform { get; set; }
    public Action onInteractEnd { get; set; }


    ItemData currentItem;

    bool isInteracting;
    public void SetIsInteracting(bool value) 
    {
        isInteracting = value;
        station.SetIsInteracting(value);
    }


    protected virtual void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        repairTask = FindObjectOfType<RepairTask>();
        myTransform = transform;
    }

    private void Start()
    {
        repairTask.onInteractEnd += InteractEnd;
        if(task != null) task.onInteractEnd += InteractEnd;
        station.onChangeStationState += ShowStationState;

        HideInteractable();
        showBroken.SetActive(false);
        showDestroyed.SetActive(false);
    }

    public void Interact()
    {
        if (isInteracting) return;

        SetIsInteracting(true);

        if (station.GetStationState() == StationState.Destroyed)
        {
            if(playerInventory.GetCurrentItem() != null && playerInventory.GetCurrentItem().name == "RepairKit")
            {
                playerInventory.RemoveCurrentItem();

                station.SetStationState(StationState.Broken);
                SetIsInteracting(false);
                Interact();
                return;
            }
            else
            {
                InteractEnd();
                return; // TODO: repair kit to fix
            }
        }
        else if (station.GetStationState() == StationState.Broken)
        {
            repairTask.StartTask(this);
            repairTask.SetTimeScale(station.timeScale);
        }
        else
        {
            if (task == null)
            {
                station.CompleteTask();
                InteractEnd();
                return;
            }

            if (neededItem == null) task.StartTask(this); // with this you can do task while carrying item
            else if (currentItem == neededItem)
            {
                task.StartTask(this);
            }
            else if (playerInventory.GetCurrentItem() == neededItem)
            {
                InsertItem(playerInventory.GetCurrentItem());
                playerInventory.SetCurrentItem(null);
                task.StartTask(this);
            }
            else
            {
                InteractEnd();
                Debug.Log("You need " + neededItem.name + " to interact with this");
            }
        }
    }

    public void InsertItem(ItemData itemData)
    {
        currentItem = itemData;
    }

    public void TakeOutItem()
    {
        playerInventory.PickUpItem(currentItem);
        
        currentItem = null;
    }

    public void ExchangeItems()
    {
        ItemData item = currentItem;
        InsertItem(playerInventory.GetCurrentItem());
        playerInventory.RemoveCurrentItem();
        
        playerInventory.PickUpItem(item);
    }

    public void InteractEnd()
    {
        if (!isInteracting) return;

        station.beingRepaired = false;
        SetIsInteracting(false);
        
        onInteractEnd.Invoke();
    }

    public void ShowInteractable()
    {
        showInteractable.SetActive(true);
    }

    public void HideInteractable()
    {
        showInteractable.SetActive(false);
    }

    void ShowStationState(StationState state) // TODO: show Destroyed
    {
        if(state == StationState.Broken) showBroken.SetActive(true);
        else showBroken.SetActive(false);

        if (state == StationState.Destroyed) showDestroyed.SetActive(true);
        else showDestroyed.SetActive(false);
    }

    public void BreakInteractable()
    {
        if(station.GetStationState() == StationState.Working) SetStationBroken();
    }

    public void SetStationBroken()
    {
        station.SetStationState(StationState.Broken);
    }

    public void FixInteractable()
    {
        InteractEnd();
        station.SetStationState(StationState.Working);
    }

    internal virtual void CompleteTask()
    {
        station.CompleteTask(currentItem);
        InteractEnd();
    }

    public void SetTimeScale(float timeScale)
    {
        station.SetTimeScale(timeScale);
        if (task != null) task.SetTimeScale(timeScale);
    }

    public void CancelTask()
    {
        repairTask.EndTask();
        if(task != null) task.EndTask();
    }

    public void StartRepair()
    {
        station.beingRepaired = true;
    }
}
