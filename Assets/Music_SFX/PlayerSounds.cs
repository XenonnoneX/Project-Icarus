using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    PlayerMovement playerMovement;
    InteractableDetector interactableDetector;
    PlayerInventory playerInventory;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip movementStepSound;
    [SerializeField] AudioClip interactSound;
    [SerializeField] AudioClip dropItem;

    bool isWalking = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        interactableDetector = FindObjectOfType<InteractableDetector>();

        interactableDetector.onInteracted += PlayInteractSound;
        playerInventory.onDropedItem += PlayDropItemSound;
    }

    private void PlayDropItemSound()
    {
        SoundManager.instance.PlaySound(dropItem);
    }

    void Start()
    {
        if (playerMovement != null)
        {
            playerMovement.OnStartWalking += OnPlayerStartWalking;
            playerMovement.OnStopWalking += OnPlayerStopWalking;
        }
    }

    private void OnPlayerStartWalking()
    {
        if (isWalking) return;
        isWalking = true;
        audioSource.Play();
    }

    private void OnPlayerStopWalking()
    {
        if (!isWalking) return;
        isWalking = false;
        audioSource.Stop();
    }

    private void PlayInteractSound()
    {
        SoundManager.instance.PlaySound(interactSound);
    }
}