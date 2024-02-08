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

    [SerializeField] AudioClip gameOverSound;

    bool goingDown = true;

    public static SpaceShipMovement instance;
    ResearchStation researchStation;
    internal float pullUpSpeedMultiplier = 1;

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
        float speed;

        float brokenPercentage = StationManager.instance.GetStationBrokenPercentage();

        if (goingDown)
        {
            speed = -(speedDown + (speedDown * brokenPercentage / percentageBrokenToCancelOutUpSpeed));
            if (brokenPercentage > percentageBrokenToCancelOutUpSpeed)
            {
                speed *= 2;
            }
        }
        else
        {
            speed = speedUp * pullUpSpeedMultiplier - (speedUp * brokenPercentage / percentageBrokenToCancelOutUpSpeed);
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

    public float GetCurrentHeightFloat()
    {
        return currentHeight;
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

        SoundManager.instance.PlaySound(gameOverSound);

        SceneManager.LoadScene("GameOver");
    }

    internal bool HeightBelowWarningHeight()
    {
        if (currentHeight < GetWarnHeight())
        {
            return true;
        }
        else return false;
    }

    internal float GetWarnHeight()
    {
        return maxHeight * 0.25f;
    }
}
