using UnityEngine;

public enum AnomalyType
{
    None,
    GravitySurge,
    TimeSlowField,
    TimeSpeedField,
    TinyBH,
    GravityField,
    Inverter,
    HITimeSpeedField
}

public class Anomaly : MonoBehaviour
{
    [SerializeField] protected float anomalyDuration = 5f;
    public float AnomalyDuration { get => anomalyDuration; }

    public AnomalyType anomalyType;

    public delegate void OnRemoveAnomaly();
    public OnRemoveAnomaly onRemoveAnomaly;

    public delegate void OnAnomalyActivated();
    public OnAnomalyActivated onAnomalyActivated;

    protected virtual void Start()
    {
        Invoke("RemoveAnomaly", anomalyDuration);

        onAnomalyActivated?.Invoke();
    }

    internal virtual void RemoveAnomaly()
    {
        onRemoveAnomaly?.Invoke();
    }
}
