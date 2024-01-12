using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    TimeManager timeManager;
    InteractableDetector interactableDetector;

    bool settingsOpen;

    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        interactableDetector = FindObjectOfType<InteractableDetector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (interactableDetector == null || interactableDetector.CurrentInteractingInteractable == null)
            {
                if (settingsOpen)
                {
                    CloseSettings();
                    settingsOpen = false;
                }
                else
                {
                    OpenSettings();
                    settingsOpen = true;
                }
            }
        }
    }

    public void OpenSettings()
    {
        // Pause the game
        if (timeManager != null) timeManager.Pause();
        
        // Load the "Settings" scene additively
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void CloseSettings()
    {

        // Unpause the game
        if (timeManager != null) timeManager.Unpause();

        // Unload the "Settings" scene
        SceneManager.UnloadSceneAsync("Settings");
    }
}