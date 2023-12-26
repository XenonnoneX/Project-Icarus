using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable, TimeAffected
{
    PlayerInventory playerInventory;
    RepairTask repairTask;
    
    public ControlStation station;
    [SerializeField] Task task;
    [SerializeField] Item neededItem;

    [SerializeField] GameObject showInteractable;
    [SerializeField] GameObject showBroken;
    [SerializeField] GameObject showDestroyed;

    public Transform myTransform { get; set; }
    public Action onInteractEnd { get; set; }


    Item currentItem;

    bool isInteracting;
    

    void Awake()
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) BreakInteractable();
    }

    public void Interact()
    {
        if (isInteracting) return;

        isInteracting = true;

        if (station.GetStationState() == StationState.Destroyed)
        {
            InteractEnd();
            return; // TODO: repair kit to fix
        }
        else if (station.GetStationState() == StationState.Broken)
        {
            print("Repairing");
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
                currentItem = playerInventory.GetCurrentItem();
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

    public void TakeItem()
    {
        if (currentItem == null) return;
        playerInventory.PickUpItem(currentItem);
        currentItem = null;
    }

    public void InteractEnd()
    {
        if (!isInteracting) return;
        
        isInteracting = false;
        
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
        station.SetStationState(StationState.Broken);
    }

    public void FixInteractable()
    {
        InteractEnd();
        station.SetStationState(StationState.Working);
    }

    internal virtual void CompleteTask()
    {
        station.CompleteTask();
        InteractEnd();
    }

    public void SetTimeScale(float timeScale)
    {
        station.SetTimeScale(timeScale);
        if (task != null) task.SetTimeScale(timeScale);
    }
}
