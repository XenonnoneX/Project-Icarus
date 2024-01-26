using System;
using UnityEngine;

public class GravityField : Anomaly
{
    PlayerMovement playerMovement;
    GravityFieldEffect gravityFieldEffect;

    Vector2 dir;
    
    [SerializeField] float pullPercentageOfMovSpeed = 0.5f;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gravityFieldEffect = FindObjectOfType<GravityFieldEffect>();
    }

    protected override void Start()
    {
        base.Start();

        SpawnSetup();
    }

    private void SpawnSetup()
    {
        int rand = UnityEngine.Random.Range(0, 2);

        if (rand == 0)
        {
            dir = new Vector2(-1, 0);
        }
        else if (rand == 1)
        {
            dir = new Vector2(1, 0);
        }
        else if(rand == 2)
        {
            dir = new Vector2(0, -1);
        }
        else if(rand == 3)
        {
            dir = new Vector2(0, 1);
        }

        playerMovement.SetAddVelocity(pullPercentageOfMovSpeed * playerMovement.MoveSpeed * dir);

        gravityFieldEffect.StartEffect(dir, anomalyDuration);
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();

        if (playerMovement != null)
        {
            playerMovement.SetAddVelocity(Vector2.zero);
        }
        gravityFieldEffect.EndEffect();

        removed = true;

        Destroy(gameObject);
    }

    bool removed;

    void OnDisable()
    {
        if(!removed) RemoveAnomaly();
    }
}