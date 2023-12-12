using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPullOnPlayer : MonoBehaviour
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gravityPullEffect.effectActive) return;

        float distance = Vector3.Distance(transform.position, playerMovement.transform.position);

        if (distance > gravityPullEffect.radius) return;

        float currentPullStrength = pullStrength * (1 - (distance / gravityPullEffect.radius));

        playerMovement.GetComponent<Rigidbody2D>().AddForce((transform.position - playerMovement.transform.position).normalized * currentPullStrength * Time.deltaTime);
    }
}
