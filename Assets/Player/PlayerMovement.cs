using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TinyBH;

public class PlayerMovement : MonoBehaviour, TimeAffected
{
    Rigidbody2D rb;
    Vector2 moveInput;
    public Vector2 MoveInput { get => moveInput; }
    
    public event Action OnStartWalking;
    public event Action OnStopWalking;

    public delegate void OnHitByBH();
    public OnHitByBH onHitByBH;

    [SerializeField] float moveSpeed = 5f;
    [HideInInspector] public float movSpeedMultiplier = 1;
    [SerializeField] float outOfShipMoveForce = 5f;

    public float timeScale = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OutOfShip())
        {
            OnStopWalking?.Invoke();

            rb.velocity += moveInput * outOfShipMoveForce * movSpeedMultiplier * timeScale;

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveSpeed * movSpeedMultiplier * timeScale);
        }
        else
        {
            if (moveInput != Vector2.zero)
            {
                rb.velocity = moveInput * moveSpeed * movSpeedMultiplier * timeScale;

                OnStartWalking?.Invoke();
            }
            else
            {
                rb.velocity = Vector2.zero;

                OnStopWalking?.Invoke();
            }
        }
    }

    private bool OutOfShip()
    {
        float rayLength = 20f; // You can adjust the ray length as needed

        // Cast rays in all directions
        for (int i = 0; i < 360; i += 10) // Change the step size as needed
        {
            // Convert angle to radians
            float angle = i * Mathf.Deg2Rad;

            // Calculate direction vector based on angle
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // Create a ray from the current position in the calculated direction
            Ray2D ray = new Ray2D(transform.position, direction);

            // Perform the raycast
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength);

            if (hit.collider == null)
            {
                // If no wall is hit, return true
                return true;
            }
        }

        // If any ray hits a wall, return false
        return false;
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    internal void AddForce(Vector3 force)
    {
        if (!this.enabled) return;
        
        rb.AddForce(force);
    }

    internal void StopMovement()
    {
        rb.velocity = Vector2.zero;
        this.enabled = false;
    }

    internal void StartMovement()
    {
        this.enabled = true;
    }

    public void GetHitByBH()
    {
        transform.position = Utils.GetRandomPosOnWalkableArea();
        rb.velocity = Vector2.zero;
        onHitByBH?.Invoke();
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}
