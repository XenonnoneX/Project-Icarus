using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<ControlStation> controlStations = new List<ControlStation>();

    Research researchStation;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        researchStation = GetComponentInChildren<Research>();
        controlStations.AddRange(GetComponentsInChildren<ControlStation>());
    }

    public void GameOver()
    {
        print("Getting " + researchStation.GetCurrentResearchPoints() + " RP");

        PlayerPrefs.SetInt("CollectedResearchPoints", researchStation.GetCurrentResearchPoints());

        SceneManager.LoadScene("Menu");
    }
}