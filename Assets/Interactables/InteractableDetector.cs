using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    PlayerMovement playerMovement;
    
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();

    IInteractable currentClosestInteractable;

    IInteractable currentInteractingInteractable;
    public IInteractable CurrentInteractingInteractable => currentInteractingInteractable;

    public delegate void OnInteracted();
    public OnInteracted onInteracted;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        playerMovement.onHitByBH += CancelTask;
    }

    private void Update()
    {
        ShowClosestInteractable();
    }

    void CancelTask()
    {
        if (currentInteractingInteractable != null) currentInteractingInteractable.CancelTask();
    }

    private void ShowClosestInteractable()
    {
        IInteractable closestInteractable = GetClosestInteractable();

        if (currentClosestInteractable != closestInteractable)
        {
            if (currentClosestInteractable != null)
            {
                currentClosestInteractable.HideInteractable();
            }

            currentClosestInteractable = closestInteractable;
            if (currentClosestInteractable != null) currentClosestInteractable.ShowInteractable();
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
        print("detector OnInvetect");

        if (currentClosestInteractable == null) return;

        currentInteractingInteractable = currentClosestInteractable;
        currentInteractingInteractable.onInteractEnd += OnInteractEnd;
        playerMovement.StopMovement();
        currentInteractingInteractable.Interact();

        onInteracted?.Invoke();
    }

    void OnTakeItem()
    {
        if (currentClosestInteractable == null) return;

        currentClosestInteractable.TakeOutItem();
    }

    public void OnInteractEnd()
    {
        currentInteractingInteractable = null;
        playerMovement.StartMovement();
    }

    IInteractable GetClosestInteractable()
    {
        float closestDistance = float.MaxValue;

        IInteractable closestInteractable = null;

        foreach (IInteractable interactable in _interactablesInRange)
        {
            if (!IsVisible(interactable)) continue;

            float distance = Vector2.Distance(transform.position, interactable.myTransform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        return closestInteractable;
    }
    private bool IsVisible(IInteractable interactable)
    {
        // cast ray to interactable

        Vector2 direction = interactable.myTransform.position - transform.position;

        // LayerMask wallLayerMask = LayerMask.GetMask("Wall");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector3.Magnitude(direction));

        if (hit.collider == null) return false;

        if (hit.collider.GetComponent<IInteractable>() == null) return false;

        if (hit.collider.GetComponent<IInteractable>() != interactable) return false;

        return true;
    }

    internal void InteractInput()
    {
        print("DoInteract");
    }
}
