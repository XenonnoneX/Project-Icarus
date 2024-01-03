using System;
using UnityEngine;

public enum AnomalyType
{
    None,
    GravitySurge,
    TimeSlowField,
    TinyBH
}

public class Anomaly : MonoBehaviour
{
    [SerializeField] protected float anomalyDuration = 5f;

    public AnomalyType anomalyType;

    protected virtual void Start()
    {
        Invoke("RemoveAnomaly", anomalyDuration);
    }

    internal virtual void RemoveAnomaly()
    {
        
    }
}
