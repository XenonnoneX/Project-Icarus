using UnityEngine;

public class MissionAvailableLight : MonoBehaviour
{
    MissionManager missionManager;

    [SerializeField] Light2D missionAvailableLight;

    private void Awake()
    {
        missionManager = FindObjectOfType<MissionManager>();
        if(missionAvailableLight == null)
        {
            missionAvailableLight = GetComponent<Light2D>();
        }
    }

    private void Update()
    {
        if (missionManager.GetStationState() != StationState.Working)
        {
            missionAvailableLight.SetAlphaMulitplier(0);
        }
        else if (missionManager.MissionAvailable())
        {
            missionAvailableLight.SetAlphaMulitplier(1);
        }
        else
        {
            missionAvailableLight.SetAlphaMulitplier(0);
        }
    }

}
