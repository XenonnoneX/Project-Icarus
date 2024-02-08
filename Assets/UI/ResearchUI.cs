using TMPro;
using UnityEngine;

public class ResearchUI : MonoBehaviour
{
    ResearchStation research;
    [SerializeField] TMP_Text PapersReleasedText;

    private void Awake()
    {
        research = FindObjectOfType<ResearchStation>();
    }

    void Update()
    {
        PapersReleasedText.text = research.PapersReleased().ToString();
    }
}
