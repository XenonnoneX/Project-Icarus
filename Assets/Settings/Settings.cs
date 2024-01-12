using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    GameObject currentPanel;

    private void Start()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        currentPanel = panels[0];
        currentPanel.SetActive(true);
    }

    public void SetPanel(int index)
    {
        if (currentPanel != null) currentPanel.SetActive(false);
        currentPanel = panels[index];
        currentPanel.SetActive(true);
    }
}
