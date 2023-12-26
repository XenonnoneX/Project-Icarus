using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    PlayerMovement playerMovement;
    
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();

    IInteractable currentClosestInteractable;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        IInteractable closestInteractable = GetClosestInteractable();

        if (currentClosestInteractable != closestInteractable)
        {
            if (currentClosestInteractable != null)
            {
                currentClosestInteractable.HideInteractable();
            }

            currentClosestInteractable = closestInteractable;
            if(currentClosestInteractable != null) currentClosestInteractable.ShowInteractable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable == null) return;

        _interactablesInRange.Add(interactable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable == null) return;

        interactable.HideInteractable();
        
        _interactablesInRange.Remove(interactable);
    }

    void OnInteract()
    {
        if (currentClosestInteractable == null) return;
        
        //currentTask = currentClosestInteractable;
        currentClosestInteractable.onInteractEnd += OnInteractEnd;
        playerMovement.StopMovement();
        currentClosestInteractable.Interact();
    }

    void OnTakeItem()
    {
        if (currentClosestInteractable == null) return;

        currentClosestInteractable.TakeItem();
    }

    public void OnInteractEnd()
    {
        if (currentClosestInteractable == null) return;

        playerMovement.StartMovement();
    }

    IInteractable GetClosestInteractable()
    {
        float closestDistance = float.MaxValue;

        IInteractable closestInteractable = null;

        foreach (IInteractable interactable in _interactablesInRange)
        {
            float distance = Vector2.Distance(transform.position, interactable.myTransform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        return closestInteractable;
    }
}
