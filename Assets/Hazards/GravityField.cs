using System;
using UnityEngine;

public class GravityField : Anomaly
{
    PlayerMovement playerMovement;
    GravityFieldEffect gravityFieldEffect;

    Vector2 dir;
    
    [SerializeField] float pullStrength = 1000f;

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
        int rand = UnityEngine.Random.Range(0, 4);

        if (rand == 0)
        {
            dir = new Vector2(0, 1);
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
            dir = new Vector2(-1, 0);
        }

        gravityFieldEffect.StartEffect(dir, anomalyDuration);
    }

    private void Update()
    {
        playerMovement.AddForce(pullStrength * Time.deltaTime * dir);
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();

        gravityFieldEffect.EndEffect();

        Destroy(gameObject);
    }
}