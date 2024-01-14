using System;
using UnityEngine;

public class GravityField : Anomaly
{
    PlayerMovement playerMovement;

    Vector2 dir;
    
    [SerializeField] float pullStrength = 1000f;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected override void Start()
    {
        base.Start();

        SpawnSetup();
    }

    private void SpawnSetup()
    {
        transform.position = Utils.GetPositionOutOfScreen(2f);

        dir = -transform.position;
    }

    private void Update()
    {
        playerMovement.AddForce(pullStrength * Time.deltaTime * dir);
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();

        Destroy(gameObject);
    }
}