using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TimeSlowField : Anomaly
{
    [SerializeField] float timeScale = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TimeAffected timeAffected = collision.GetComponent<TimeAffected>();

        if (timeAffected == null) return;

        timeAffected.SetTimeScale(timeScale);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TimeAffected timeAffected = collision.GetComponent<TimeAffected>();

        if (timeAffected == null) return;

        timeAffected.SetTimeScale(1f);
    }
}
