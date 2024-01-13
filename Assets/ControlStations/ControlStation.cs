using System;
using UnityEngine;

public enum StationType
{
    ShipMovement,
    Research,
    MissionManager,
    AnomalyAnalizer,
    ArtifactDock,
    Door
}

public enum StationState
{
    Working,
    Broken,
    Destroyed
}

public abstract class ControlStation : MonoBehaviour, TimeAffected
{
    public StationType stationType;
    public Color stationColor;
    [SerializeField]protected StationState stationState;

    public delegate void OnChangeStationState(StationState stationState);
    public OnChangeStationState onChangeStationState;

    public delegate void OnCompleteTask();
    public OnCompleteTask onCompleteTask;

    bool isInteracting;

    public float timeScale = 1;

    [SerializeField] float timeToBreak = 30;
    float timeSinceBroken = 0;
    public bool beingRepaired;
    bool canBreak => stationState == StationState.Working && !beingRepaired && timeSinceRepaired > safeTimeAfterRepair && !isInteracting;
    [SerializeField] float safeTimeAfterRepair = 10f;
    float timeSinceRepaired;

    protected virtual void Start()
    {
        timeSinceRepaired = safeTimeAfterRepair;
    }

    protected virtual void Update()
    {
        if(stationState == StationState.Broken && !beingRepaired)
        {
            timeSinceBroken += Time.deltaTime * timeScale;
            if(timeSinceBroken > timeToBreak)
            {
                SetStationState(StationState.Destroyed);
            }
        }
        else
        {
            if (stationState == StationState.Working && !canBreak)
            {
                timeSinceRepaired += Time.deltaTime * timeScale;
            }
        }
    }

    public void SetStationState(StationState state)
    {
        if ((state == StationState.Broken || state == StationState.Destroyed) && !canBreak) return;
        
        stationState = state;

        if (stationState == StationState.Working)
        {
            timeSinceBroken = 0;
        }

        onChangeStationState?.Invoke(state);
    }

    public StationState GetStationState()
    {
        return stationState;
    }

    public virtual void CompleteTask()
    {
        onCompleteTask?.Invoke();
        print("Task completed");
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }

    public float GetBrokenTimePercentage()
    {
        return timeSinceBroken / timeToBreak;
    }

    internal bool CanBreak()
    {
        if (stationState != StationState.Working) print("Not Working");
        if (beingRepaired) print("beingRepaired");
        if (timeSinceRepaired <= safeTimeAfterRepair) print("SafeTime");
        if (isInteracting) print("IsInteracting");
        return canBreak;
    }

    internal void SetIsInteracting(bool value)
    {
        isInteracting = value;
    }
}
