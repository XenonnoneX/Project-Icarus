using UnityEngine;
using UnityEngine.UI;

public class RPStorageUI : MonoBehaviour
{
    ResearchStation researchStation;

    [SerializeField] Image fillImage;
    [SerializeField] Image secondFillImage;
    [SerializeField] Light2D warningLight;

    private void Awake()
    {
        researchStation = FindObjectOfType<ResearchStation>();
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.fillAmount = researchStation.GetRPStorageFillPercentage();
        secondFillImage.fillAmount = researchStation.GetRPStorageFillPercentage();
        
        if (researchStation.GetStationState() != StationState.Working)
        {
            warningLight.SetAlphaMulitplier(0);
        }
        else if (researchStation.GetRPStorageFillPercentage() >= 0.75f)
        {
            warningLight.SetAlphaMulitplier(1);
        }
        else
        {
            warningLight.SetAlphaMulitplier(0);
        }
    }
}