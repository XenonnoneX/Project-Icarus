using UnityEngine;

public class HeightWarnLight : MonoBehaviour
{
    SpaceShipMovement spaceShipMovement;

    [SerializeField] Light2D heightWarnLight;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
        if (heightWarnLight == null)
        {
            heightWarnLight = GetComponent<Light2D>();
        }
    }

    private void Update()
    {
        if (spaceShipMovement.GetStationState() != StationState.Working)
        {
            heightWarnLight.SetAlphaMulitplier(0);
        }
        else if (spaceShipMovement.HeightBelowWarningHeight())
        {
            heightWarnLight.SetAlphaMulitplier(1);
        }
        else
        {
            heightWarnLight.SetAlphaMulitplier(0);
        }
    }
}