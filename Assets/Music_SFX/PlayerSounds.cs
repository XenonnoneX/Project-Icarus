using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    PlayerMovement playerMovement;

    public AudioClip movementStepSound;
    public AudioClip interactSound;

    bool isWalking = false;
     
    [SerializeField] float stepSoundDelay = 0.5f;
    float stepSoundTimer = 0f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (playerMovement != null)
        {
            playerMovement.OnStartWalking += OnPlayerStartWalking;
            playerMovement.OnStopWalking += OnPlayerStopWalking;
        }
    }

    private void Update()
    {
        stepSoundTimer += Time.deltaTime * playerMovement.timeScale;

        if (stepSoundTimer > stepSoundDelay)
        {
            stepSoundTimer = 0;
            if (isWalking) SoundManager.instance.PlaySound(movementStepSound);
        }
    }

    private void OnPlayerStartWalking()
    {
        if (isWalking) return;
        isWalking = true;
    }

    private void OnPlayerStopWalking()
    {
        if (!isWalking) return;
        isWalking = false;
    }
}