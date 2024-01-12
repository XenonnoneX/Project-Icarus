using UnityEngine;

public class ArtifactAvailableLight : MonoBehaviour
{
    ArtifactDock artifactDock;

    [SerializeField] Light2D artifactAvailableLight;

    private void Awake()
    {
        artifactDock = FindObjectOfType<ArtifactDock>();
        if (artifactAvailableLight == null)
        {
            artifactAvailableLight = GetComponent<Light2D>();
        }
    }

    private void Update()
    {
        if (artifactDock.GetStationState() != StationState.Working)
        {
            artifactAvailableLight.SetAlphaMulitplier(0);
        }
        else if (artifactDock.ArtifactAvailable())
        {
            artifactAvailableLight.SetAlphaMulitplier(1);
        }
        else
        {
            artifactAvailableLight.SetAlphaMulitplier(0);
        }
    }
}
