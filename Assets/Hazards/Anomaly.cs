using UnityEngine;

public enum AnomalyType
{
    None,
    GravitySurge,
    TimeSlowField,
    TimeSpeedField,
    TinyBH
}

public class Anomaly : MonoBehaviour
{
    [SerializeField] protected float anomalyDuration = 5f;

    public AnomalyType anomalyType;

    public delegate void OnRemoveAnomaly();
    public OnRemoveAnomaly onRemoveAnomaly;

    protected virtual void Start()
    {
        Invoke("RemoveAnomaly", anomalyDuration);
    }

    internal virtual void RemoveAnomaly()
    {
        onRemoveAnomaly?.Invoke();
    }
}
