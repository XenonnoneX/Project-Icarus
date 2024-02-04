using System;
using UnityEngine;

public enum StationType
{
    ShipMovement,
    Research,
    MissionManager,
    AnomalyAnalizer,
    ArtifactDock,
    Door,
    Telescope
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
    internal float timeToBreakMultiplier = 1;
    float timeSinceBroken = 0;
    public bool beingRepaired;
    bool canBreak => !beingRepaired && timeSinceRepaired >= safeTimeAfterRepair && !isInteracting;
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
            if (timeSinceBroken > timeToBreak * timeToBreakMultiplier)
            {
                SetStationState(StationState.Destroyed);
            }
        }
        else
        {
            if (stationState == StationState.Working && !CanBreak())
            {
                timeSinceRepaired += Time.deltaTime * timeScale;
            }
        }
    }

    public void SetStationState(StationState state)
    {
        if (state == stationState) return;

        if (state == StationState.Broken && stationState == StationState.Working && !CanBreak()) return;

        if (state == StationState.Destroyed && !CanBreak()) return;
        
        stationState = state;

        if (stationState == StationState.Working)
        {
            timeSinceRepaired = 0;
        }else if(stationState == StationState.Broken)
        {
            timeSinceBroken = 0;
        }

        onChangeStationState?.Invoke(state);
    }

    public StationState GetStationState()
    {
        return stationState;
    }

    public virtual void CompleteTask(ItemData currentItem = null)
    {
        onCompleteTask?.Invoke();
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
        return canBreak;
    }

    internal void SetIsInteracting(bool value)
    {
        isInteracting = value;
    }
}
