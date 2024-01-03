using UnityEngine;
using UnityEngine.UI;

public class RPStorageUI : MonoBehaviour
{
    ResearchStation researchStation;

    [SerializeField] Image image;
    [SerializeField] Light2D warningLight;

    private void Awake()
    {
        researchStation = FindObjectOfType<ResearchStation>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = researchStation.GetRPStorageFillPercentage();
        warningLight.SetAlphaMulitplier(Mathf.Min(researchStation.GetRPStorageFillPercentage() + 0.5f, 1));
    }
}