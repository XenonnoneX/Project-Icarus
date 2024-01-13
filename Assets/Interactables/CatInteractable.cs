using System;
using System.Collections.Generic;
using UnityEngine;

public class CatInteractable : MonoBehaviour, IInteractable, TimeAffected
{
    InteractableManager interactableManager;
    PlayerMovement playerMovement;

    List<Interactable> interactables;
    
    [SerializeField] GameObject showInteractable;

    [SerializeField] float cooldown = 10f;
    float cdTimer;

    float timeScale;

    public Transform myTransform { get; set; }
    public Action onInteractEnd { get; set; }

    void Awake()
    {
        interactableManager = FindObjectOfType<InteractableManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        interactables = interactableManager.GetInteractables();

        myTransform = transform;
    }

    void Update()
    {
        cdTimer += Time.deltaTime * timeScale;
    }

    public void ShowInteractable()
    {
        showInteractable.SetActive(true);
    }

    public void HideInteractable()
    {
        showInteractable.SetActive(false);
    }

    public void Interact()
    {
        TeleportPlayerToClosestBrokenStation();
        InteractEnd();
    }

    public void InteractEnd()
    {
        onInteractEnd?.Invoke();
    }

    public void CancelTask()
    {
        onInteractEnd?.Invoke();
    }

    public void TakeItem()
    {

    }

    private void TeleportPlayerToClosestBrokenStation()
    {
        if (cdTimer < cooldown)
        {
            return;
        }

        List<Interactable> brokenInteractables = new List<Interactable>();

        foreach (Interactable interactable in interactables)
        {
            if (interactable.station.GetStationState() == StationState.Broken)
            {
                brokenInteractables.Add(interactable);
            }
        }

        if (brokenInteractables.Count == 0) return;

        cdTimer = 0;

        int rand = UnityEngine.Random.Range(0, brokenInteractables.Count);

        playerMovement.TeleportToInteractable(brokenInteractables[rand]);
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}