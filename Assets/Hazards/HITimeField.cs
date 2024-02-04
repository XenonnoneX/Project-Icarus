using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HITimeField : Anomaly
{
    HITimeSpeedEffect hiTimeSpeedEffect;

    [SerializeField] float sizeIncreasePerSecond = 3f;
    float currentSize = 0f;

    [SerializeField] float timeScale = 0.5f;

    List<TimeAffected> currentlyAffectedObjects = new List<TimeAffected>();


    private void Awake()
    {
        hiTimeSpeedEffect = Camera.main.GetComponent<HITimeSpeedEffect>();
    }

    protected override void Start()
    {
        base.Start();
        
        hiTimeSpeedEffect.StartEffect(transform, anomalyDuration);
    }

    private void Update()
    {
        currentSize += sizeIncreasePerSecond * Time.deltaTime;

        transform.localScale = new Vector3(currentSize, currentSize, 1f);

        hiTimeSpeedEffect.SetRadius(currentSize);
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
