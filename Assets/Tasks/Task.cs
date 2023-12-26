using System;
using UnityEngine;

public abstract class Task : MonoBehaviour, TimeAffected
{
    protected PlayerInputs controls;
    
    [SerializeField] protected GameObject showTaskStuff;

    protected Interactable interactable;

    protected bool isInteracting;
    public Action onInteractEnd { get; set; }

    protected float timeScale = 1;

    protected virtual void Start()
    {
        controls = new PlayerInputs();
        controls.Enable();
        if(showTaskStuff != null) showTaskStuff.SetActive(false);
    }

    protected virtual void Update()
    {
        if (controls.Player.CancelInteraction.triggered)
        {
            EndTask();
        }

        if (!isInteracting) return;

        UpdateLogic();
    }

    protected virtual void UpdateLogic()
    {
        
    }

    public virtual void StartTask(Interactable interactable)
    {
        if (showTaskStuff != null) showTaskStuff.SetActive(true);
        isInteracting = true;

        this.interactable = interactable;
    }

    public virtual void EndTask()
    {
        if (showTaskStuff != null) showTaskStuff.SetActive(false);
        isInteracting = false;

        onInteractEnd?.Invoke();
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}