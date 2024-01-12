using UnityEngine;

public class GravityPullOnPlayer : Anomaly
{
    GravityPullEffect gravityPullEffect;
    PlayerMovement playerMovement;

    [SerializeField] float pullStrength;

    private void Awake()
    {
        gravityPullEffect = FindObjectOfType<GravityPullEffect>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gravityPullEffect.StartEffect(transform, anomalyDuration);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerMovement.transform.position);

        if (distance > gravityPullEffect.radius) return;

        float currentPullStrength = pullStrength * (1 - (distance / gravityPullEffect.radius));

        playerMovement.AddForce((transform.position - playerMovement.transform.position).normalized * currentPullStrength * Time.deltaTime);
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();
        gravityPullEffect.EndEffect();
    }
}
