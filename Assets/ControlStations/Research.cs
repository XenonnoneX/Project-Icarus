using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Research : ControlStation
{
    SpaceShipMovement spaceShipMovement;
    
    float currentResearchPoints;

    public int GetCurrentResearchPoints()
    {
        return (int) currentResearchPoints;
    }

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBroken) return;

        AddResearchPoints(ResearchPointsPerSecond() * Time.deltaTime);
    }

    private float ResearchPointsPerSecond()
    {
        int shipHeight = spaceShipMovement.GetCurrentHeight() + 1; // to not divide by 0

        return 10f / shipHeight;
    }

    public void AddResearchPoints(float researchPoints)
    {
        currentResearchPoints += researchPoints;
    }
}
