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

    float timeScale = 1;

    public Transform myTransform { get; set; }
    public Action onInteractEnd { get; set; }

    void Awake()
    {
        interactableManager = FindObjectOfType<InteractableManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        interactables = interactableManager.GetInteractables();

        myTransform = transform;
    }

    private void Start()
    {
        HideInteractable();

        cdTimer = cooldown;
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

    public void TakeOutItem()
    {

    }

    private void TeleportPlayerToClosestBrokenStation()
    {
        if (cdTimer < cooldown)
        {
            return;
        }

        Interactable mostBrokenInteractable = null;
        float longestBrokenTime = 0;

        foreach (Interactable interactable in interactables)
        {
            if (interactable.station.GetStationState() == StationState.Broken)
            {
                if (interactable.station.TimeSinceBroken > longestBrokenTime)
                {
                    mostBrokenInteractable = interactable;
                    longestBrokenTime = interactable.station.TimeSinceBroken;
                }
            }
        }

        if (mostBrokenInteractable == null) return;

        cdTimer = 0;

        playerMovement.TeleportToInteractable(mostBrokenInteractable);
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}