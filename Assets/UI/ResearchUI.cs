using TMPro;
using UnityEngine;

public class ResearchUI : MonoBehaviour
{
    Research research;
    [SerializeField] TMP_Text currentResearchPointsText;

    private void Awake()
    {
        research = FindObjectOfType<Research>();
    }

    void Update()
    {
        currentResearchPointsText.text = research.GetCurrentResearchPoints().ToString() + " RP";
    }
}
