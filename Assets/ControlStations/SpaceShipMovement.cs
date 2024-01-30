using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipMovement : ControlStation
{
    [SerializeField] float minHeight, maxHeight;

    [SerializeField] float speedUp = 1f;
    [SerializeField] float speedDown = 0.1f;
    [SerializeField] float percentageBrokenToCancelOutUpSpeed = 0.7f;

    [SerializeField] float currentHeight;

    bool goingDown = true;

    public static SpaceShipMovement instance;
    ResearchStation researchStation;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        researchStation = StationManager.instance.GetControlStationFromType(StationType.Research) as ResearchStation;
    }

    protected override void Update()
    {
        base.Update();

        currentHeight += GetCurrentSpeed() * Time.deltaTime;

        if (currentHeight > maxHeight)
        {
            currentHeight = maxHeight;
        }
        else if (currentHeight < minHeight)
        {
            GameOver();
        }
    }

    float GetCurrentSpeed()
    {
        float speed = 0;

        float brokenPercentage = StationManager.instance.GetStationBrokenPercentage();

        if (goingDown)
        {
            speed -= speedDown + (speedDown * brokenPercentage / percentageBrokenToCancelOutUpSpeed);
            if (brokenPercentage > percentageBrokenToCancelOutUpSpeed)
            {
                speed *= 2;
            }
        }
        else
        {
            speed += speedUp - (speedUp * brokenPercentage / percentageBrokenToCancelOutUpSpeed);
        }
        return speed;
    }

    public void GoUp()
    {
        goingDown = false;
    }

    public void GoDown()
    {
        goingDown = true;
    }

    public int GetCurrentHeight()
    {
        return (int)currentHeight;
    }

    internal float GetCurrentHeightPercentage()
    {
        return (currentHeight - minHeight) / maxHeight;
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("CollectedResearchPoints", researchStation.GetSavedRP());
        PlayerPrefs.SetInt("ReleasedPapers", researchStation.PapersReleased());
        PlayerPrefs.SetFloat("TimeSurvived", Time.timeSinceLevelLoad);

        SceneManager.LoadScene("GameOver");
    }

    internal bool HeightBelowWarningHeight()
    {
        if (currentHeight < maxHeight * 0.25f)
        {
            return true;
        }
        else return false;
    }
}
