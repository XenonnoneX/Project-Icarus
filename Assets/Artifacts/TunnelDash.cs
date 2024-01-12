using UnityEngine;

public class TunnelDash : Dash
{
    [SerializeField] Dash dash;
    
    [SerializeField] float tunnelDashCD = 5;

    float timeSinceLastTunnelDash = Mathf.Infinity;

    private void Start()
    {
        dashCurve = dash.dashCurve;
        dashRange = dash.dashRange;
        dashDuration = dash.dashDuration;
        cooldown = dash.cooldown;
    }

    protected override void Update()
    {
        base.Update();

        timeSinceLastTunnelDash += Time.deltaTime;
    }

    protected override void EnableArtifact()
    {
        base.EnableArtifact();

        dash.enabled = false;
    }

    protected override void AbilityLogic()
    {
        if (timeSinceLastTunnelDash >= tunnelDashCD)
        {
            Teleport();
            timeSinceLastTunnelDash = 0;
        }
        else
        {
            base.AbilityLogic();
        }
    }

    private void Teleport()
    {
        playerMovement.gameObject.transform.position += Utils.V2_To_V3(playerMovement.MoveInput * dash.dashRange);
    }
}
