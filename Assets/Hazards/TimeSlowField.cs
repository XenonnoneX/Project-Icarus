using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TimeSlowField : Anomaly
{
    [SerializeField] float timeScale = 0.5f;

    List<TimeAffected> currentlyAffectedObjects = new List<TimeAffected>();


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
        foreach (TimeAffected timeAffected in currentlyAffectedObjects)
        {
            timeAffected.SetTimeScale(1f);
        }
        
        Destroy(gameObject);
    }
}
