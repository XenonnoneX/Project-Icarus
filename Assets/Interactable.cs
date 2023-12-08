using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactionPanel;
    [SerializeField] GameObject showInteractable;

    private void Start()
    {
        interactionPanel.SetActive(false);
        showInteractable.SetActive(false);
    }

    public void Interact()
    {
        interactionPanel.SetActive(true);
    }

    public void InteractEnd()
    {
        interactionPanel.SetActive(false);
    }

    public void ShowInteractable()
    {
        showInteractable.SetActive(true);
    }

    public void HideInteractable()
    {
        showInteractable.SetActive(false);
    }
}
