using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TimeSlowField : Anomaly
{
    TimeSlowEffect timeSlowEffect;
    TimeSpeedEffect timeSpeedEffect;

    [SerializeField] float timeScale = 0.5f;

    List<TimeAffected> currentlyAffectedObjects = new List<TimeAffected>();


    private void Awake()
    {
        timeSlowEffect = Camera.main.GetComponent<TimeSlowEffect>();
        timeSpeedEffect = Camera.main.GetComponent<TimeSpeedEffect>();
    }

    protected override void Start()
    {
        base.Start();

        if (timeScale < 1)
        {
            timeSlowEffect.StartEffect(transform, anomalyDuration);
        }
        else
        {
            timeSpeedEffect.StartEffect(transform, anomalyDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TimeAffected timeAffected = collision.GetComponent<TimeAffected>();

        if (timeAffected == null) return;

        timeAffected.SetTimeScale(timeScale);

        currentlyAffectedObjects.Add(timeAffected);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TimeAffected timeAffected = collision.GetComponent<TimeAffected>();

        if (timeAffected == null) return;

        timeAffected.SetTimeScale(1f);

        currentlyAffectedObjects.Remove(timeAffected);
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();

        foreach (TimeAffected timeAffected in currentlyAffectedObjects)
        {
            timeAffected.SetTimeScale(1f);
        }
        
        Destroy(gameObject);
    }
}
