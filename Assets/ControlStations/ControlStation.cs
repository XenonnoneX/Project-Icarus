using System;
using UnityEngine;

public abstract class ControlStation : MonoBehaviour
{
    public StationType stationType;
    [SerializeField]protected bool isBroken;

    public delegate void OnChangeBrokenState(bool isBroken);
    public OnChangeBrokenState onChangeBrokenState;

    public delegate void OnCompleteTask();
    public OnCompleteTask onCompleteTask;

    public void SetIsBroken(bool isBroken)
    {
        this.isBroken = isBroken;

        onChangeBrokenState?.Invoke(isBroken);
    }

    public bool GetIsBroken()
    {
        return isBroken;
    }

    internal virtual void CompleteTask()
    {
        onCompleteTask?.Invoke();
        print("Task completed");
    }
}