using UnityEngine;
using UnityEngine.UI;

public class RPStorageUI : MonoBehaviour
{
    ResearchStation researchStation;

    [SerializeField] Image image;

    private void Awake()
    {
        researchStation = FindObjectOfType<ResearchStation>();
    }

    // Update is called once per frame
    void Update()
    {
        image.GetComponent<RectTransform>().localScale = new Vector3(1,researchStation.GetRPStorageFillPercentage(),1);
    }
}