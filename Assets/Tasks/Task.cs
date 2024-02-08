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

    public delegate void OnStartTask(float timeScale);
    public event OnStartTask onStartTask;

    protected virtual void Start()
    {
        controls = new PlayerInputs();
        controls.Enable();
        if(showTaskStuff != null) showTaskStuff.SetActive(false);
    }

    protected virtual void LateUpdate()
    {
        if (!isInteracting) return;

        if (controls.Player.CancelInteraction.triggered)
        {
            EndTask();
        }

        UpdateLogic();
    }

    protected virtual void UpdateLogic()
    {
        
    }

    public virtual void StartTask(Interactable interactable)
    {
        if (showTaskStuff != null) showTaskStuff.SetActive(true);

        this.interactable = interactable;

        SetIsInteracting(true);
        
        onStartTask?.Invoke(timeScale);
    }

    public virtual void EndTask()
    {
        if (showTaskStuff != null)
        {
            showTaskStuff.SetActive(false);
        }

        onInteractEnd?.Invoke();

        SetIsInteracting(false);
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }

    void SetIsInteracting(bool isInteracting)
    {
        this.isInteracting = isInteracting;
        if(interactable != null) interactable.SetIsInteracting(isInteracting);
    }
}
