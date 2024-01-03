using UnityEngine;

public class ShowBroken : MonoBehaviour
{
    [SerializeField] Interactable interactable;
    ControlStation station;
    [SerializeField] Light2D warningLight;

    private void Awake()
    {
        station = interactable.station;
    }

    private void Update()
    {
        float brokenPercentage = station.GetBrokenTimePercentage();

        warningLight.SetBlinkSpeedMultiplier(brokenPercentage);
        warningLight.SetAlphaMulitplier(brokenPercentage);
        warningLight.SetTimeScale(station.timeScale);
    }

}