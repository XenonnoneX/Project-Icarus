using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public float timeScale = 1f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale; 
        Time.timeScale = timeScale;
    }

    internal void Pause()
    {
        SetTimeScale(0);
    }

    internal void Unpause()
    {
        SetTimeScale(1);
    }
}
