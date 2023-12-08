using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    BrokenInteractablePanel brokenInteractablePanel;
    [SerializeField] GameObject interactionPanel;
    [SerializeField] Breakable station;
    [SerializeField] GameObject showInteractable;

    public Transform myTransform { get; set; }

    [SerializeField] bool isBroken = false;

    

    void Awake()
    {
        brokenInteractablePanel = FindObjectOfType<BrokenInteractablePanel>();
    }

    private void Start()
    {
        myTransform = transform;
        interactionPanel.SetActive(false);
        showInteractable.SetActive(false);

        if (isBroken) BreakInteractable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) BreakInteractable();
    }

    public void Interact()
    {
        if (isBroken)
        {
            brokenInteractablePanel.gameObject.SetActive(true);
            brokenInteractablePanel.SetCurrentInteractable(this);
        }
        else
        {
            interactionPanel.SetActive(true);
        }
    }

    public void InteractEnd()
    {
        if (isBroken) brokenInteractablePanel.gameObject.SetActive(false);
        else interactionPanel.SetActive(false);
    }

    public void ShowInteractable()
    {
        showInteractable.SetActive(true);
    }

    public void HideInteractable()
    {
        showInteractable.SetActive(false);
    }

    public void BreakInteractable()
    {
        isBroken = true;
        station.Break();
    }

    public void FixInteractable()
    {
        InteractEnd();
        isBroken = false;
        station.Fix();
    }
}
