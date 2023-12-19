using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    PlayerInventory playerInventory;
    RepairTask repairTask;
    
    [SerializeField] ControlStation station;
    [SerializeField] Task task;
    [SerializeField] Item neededItem;

    [SerializeField] GameObject showInteractable;
    [SerializeField] GameObject showBroken;

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
        task.onInteractEnd += InteractEnd;
        station.onChangeBrokenState += SetBrokenShowActive;

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
        
        if (station.GetIsBroken())
        {
            print("Repairing");
            repairTask.StartTask(this);
        }
        else
        {
            // if (neededItem == null) task.StartTask(this); else if // with this you can do task while carrying item
            if (currentItem == neededItem)
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

    void SetBrokenShowActive(bool active)
    {
        showBroken.SetActive(active);
    }

    public void BreakInteractable()
    {
        station.SetIsBroken(true);
    }

    public void FixInteractable()
    {
        InteractEnd();
        station.SetIsBroken(false);
    }

    internal virtual void CompleteTask()
    {
        station.CompleteTask();
        InteractEnd();
    }
}
