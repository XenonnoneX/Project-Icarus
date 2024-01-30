using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControlsAnomaly : Anomaly
{
    PlayerMovement playerMovement;
    InvertEffect invertEffect;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        invertEffect = FindObjectOfType<InvertEffect>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerMovement.SetControlsInverted(true);

        invertEffect.StartEffect(anomalyDuration);
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();
        playerMovement.SetControlsInverted(false);

        invertEffect.EndEffect();
    }
}
