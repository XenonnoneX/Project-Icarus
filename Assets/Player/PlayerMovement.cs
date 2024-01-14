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
        if (Utils.OutOfShip(transform))
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

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    internal void AddForce(Vector3 force)
    {
        if (!this.enabled) return;
        
        rb.AddForce(force * timeScale);
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

    internal void TeleportToInteractable(Interactable interactable)
    {
        transform.position = Utils.GetWalkablePosNextTo(interactable.transform.position, 1f);
        rb.velocity = Vector2.zero;
    }
}
