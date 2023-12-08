using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Research : Breakable
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

        currentResearchPoints += ResearchPointsPerSecond() * Time.deltaTime;
    }

    private float ResearchPointsPerSecond()
    {
        int shipHeight = spaceShipMovement.GetCurrentHeight();

        return 10f / shipHeight;
    }
}
