using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) TogglePause();

        Time.timeScale = timeScale;
    }

    public void SetTimeScale(float scale)
    {
        timeScale = scale;
    }

    public void TogglePause()
    {
        if (timeScale == 0) timeScale = 1;
        else timeScale = 0;
    }
}
