using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    PlayerMovement playerMovement;
    
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();

    IInteractable currentInteractable;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        IInteractable closestInteractable = GetClosestInteractable();

        if (currentInteractable != closestInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.HideInteractable();
            }

            currentInteractable = closestInteractable;
            if(currentInteractable != null) currentInteractable.ShowInteractable();
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
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            currentInteractable.onInteractEnd += OnInteractEnd;
            playerMovement.enabled = false;
        }
    }

    public void OnInteractEnd()
    {
        if (currentInteractable != null)
        {
            currentInteractable.InteractEnd();
            playerMovement.enabled = true;
        }
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