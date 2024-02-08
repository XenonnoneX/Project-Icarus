
public class HILowGravity : Anomaly
{
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected override void Start()
    {
        base.Start();

        playerMovement.everywhereLowGravity = true;
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();

        playerMovement.everywhereLowGravity = false;
    }
}
