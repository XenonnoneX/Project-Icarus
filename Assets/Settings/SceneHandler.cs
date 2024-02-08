using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    protected PlayerInputs controls;
    
    TimeManager timeManager;
    InteractableDetector interactableDetector;

    bool settingsOpen;
    int settingsStartIndex = 0;
    public int SettingsStartIndex => settingsStartIndex;

    private void Start()
    {
        controls = new PlayerInputs();
        controls.Enable();
        
        timeManager = FindObjectOfType<TimeManager>();
        interactableDetector = FindObjectOfType<InteractableDetector>();
    }

    private void Update()
    {
        if (controls.Player.CancelInteraction.triggered)
        {
            if (interactableDetector == null || interactableDetector.CurrentInteractingInteractable == null)
            {
                if (settingsOpen)
                {
                    CloseSettings();
                }
                else
                {
                    OpenSettings();
                }
            }
        }
    }

    public void OpenSettings(int index = 0)
    {
        settingsOpen = true;
        settingsStartIndex = index;
            
        // Pause the game
        if (timeManager != null) timeManager.Pause();

        // Start a coroutine to wait for the scene to load before setting the selected button
        StartCoroutine(WaitForSceneLoadAndSetSelectedButton("SettingsEventSystem", index));
    }

    private IEnumerator WaitForSceneLoadAndSetSelectedButton(string eventSystemName, int index)
    {
        // Wait until the "Settings" scene is fully loaded
        yield return SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);

        // Find the EventSystem in the specified scene
        EventSystem eventSystem = GameObject.Find(eventSystemName).GetComponent<EventSystem>();

        // Get the Selectable component of the button at the specified index
        Selectable selectable = eventSystem.firstSelectedGameObject.GetComponent<Selectable>();

        // Set the selected button
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(selectable.gameObject);
    }

    public void CloseSettings()
    {
        settingsOpen = false;

        // Unpause the game
        if (timeManager != null) timeManager.Unpause();

        // Unload the "Settings" scene
        SceneManager.UnloadSceneAsync("Settings");

        SetSelectedButtonToFirstSelected("EventSystem");
    }

    public void Quit()
    {
        Application.Quit();
    }
    private void SetSelectedButtonToFirstSelected(string eventSystemName)
    {
        // Find the EventSystem in the specified scene
        EventSystem eventSystem = GameObject.Find(eventSystemName).GetComponent<EventSystem>();

        if (eventSystem.firstSelectedGameObject == null) return;

        // Get the Selectable component of the button at the specified index
        Selectable selectable = eventSystem.firstSelectedGameObject.GetComponent<Selectable>();

        // Set the selected button
        eventSystem.SetSelectedGameObject(selectable.gameObject);
    }
}