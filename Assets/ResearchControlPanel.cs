using TMPro;
using UnityEngine;

public class ResearchControlPanel : MonoBehaviour
{
    [SerializeField] Research research;
    [SerializeField] TMP_Text currentResearchPointsText;

    void Update()
    {
        currentResearchPointsText.text = research.GetCurrentResearchPoints().ToString();
    }
}
