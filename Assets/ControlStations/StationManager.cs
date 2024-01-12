using System.Collections.Generic;
using UnityEngine;


public class StationManager : MonoBehaviour
{
    public static StationManager instance;

    public List<ControlStation> controlStations = new List<ControlStation>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        controlStations.AddRange(GetComponentsInChildren<ControlStation>());
    }

    public float GetStationBrokenPercentage()
    {
        float percentage = 0;

        foreach (ControlStation station in controlStations)
        {
            if (station.GetStationState() == StationState.Broken) percentage += 0.5f;
            else if(station.GetStationState() == StationState.Destroyed) percentage += 1f;
        }

        return percentage / controlStations.Count;
    }

    public ControlStation GetControlStationFromType(StationType type)
    {
        foreach (ControlStation station in controlStations)
        {
            if (station.stationType == type) return station;
        }

        return null;
    }
}
