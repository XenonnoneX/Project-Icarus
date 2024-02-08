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
    HITimeSpeedField,
    HILowGravity
}

public class Anomaly : MonoBehaviour
{
    [SerializeField] protected float anomalyDuration = 5f;
    public float AnomalyDuration { get => anomalyDuration; }

    public AnomalyType anomalyType;
    public Sprite warningCountdownSprite;
    public Sprite warningImpactNowSprite;

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
