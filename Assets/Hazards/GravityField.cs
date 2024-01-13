using System;
using UnityEngine;

public class GravityField : Anomaly
{
    PlayerMovement playerMovement;

    Vector2 dir;

    [SerializeField] float warningTime = 5f;
    float warningTimer;
    [SerializeField] float pullStrength = 10f;

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
        if(warningTimer > warningTime) playerMovement.AddForce(pullStrength * Time.deltaTime * dir);
        else warningTimer += Time.deltaTime;
    }

    internal override void RemoveAnomaly()
    {
        base.RemoveAnomaly();

        Destroy(gameObject);
    }
}

public class GravityFieldWarning : MonoBehaviour
{
    [SerializeField] GameObject showWarning;

    private void Start()
    {
        showWarning.SetActive(true);
    }

    
}