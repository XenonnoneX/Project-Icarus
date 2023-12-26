using TMPro;
using UnityEngine;

public class ResearchUI : MonoBehaviour
{
    ResearchStation research;
    [SerializeField] TMP_Text currentResearchPointsText;

    private void Awake()
    {
        research = FindObjectOfType<ResearchStation>();
    }

    void Update()
    {
        currentResearchPointsText.text = research.GetSavedRP().ToString() + " RP";
    }
}
