using System;
using UnityEngine;

public enum StationType
{
    ShipMovement,
    Research,
    MissionManager,
    SendSignal,
    ArtifactDock
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

    public float timeScale = 1;

    float timeToBreak = 30;
    float timeSinceBroken = 0;

    protected virtual void Update()
    {
        if(stationState == global::StationState.Broken)
        {
            timeSinceBroken += Time.deltaTime * timeScale;
            if(timeSinceBroken > timeToBreak)
            {
                SetStationState(StationState.Destroyed);
            }
        }
    }

    public void SetStationState(StationState state)
    {
        this.stationState = state;

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
}