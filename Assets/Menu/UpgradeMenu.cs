using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    public int currentResearchPoints;

    public delegate void OnResearchPointsChanged();
    public OnResearchPointsChanged onResearchPointsChanged;

    private void Start()
    {
        currentResearchPoints = PlayerPrefs.GetInt("TotalResearchPoints");

        print("previous: " + currentResearchPoints);

        currentResearchPoints += PlayerPrefs.GetInt("CollectedResearchPoints");

        print("new: " + currentResearchPoints);

        onResearchPointsChanged += SaveResearchPoints;
        onResearchPointsChanged?.Invoke();
    }

    void SaveResearchPoints()
    {
        PlayerPrefs.SetInt("TotalResearchPoints", currentResearchPoints);
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
