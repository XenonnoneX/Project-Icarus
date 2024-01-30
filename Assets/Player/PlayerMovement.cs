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
    public event Action OnLoopBoundaries;

    public delegate void OnHitByBH();
    public OnHitByBH onHitByBH;

    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed { get => moveSpeed; }
    [HideInInspector] public float movSpeedMultiplier = 1;
    [SerializeField] float outOfShipMoveForce = 5f;
    [SerializeField] Vector2 distToLoopToOtherSide;

    Vector2 moveSpeedAddition;
    bool controlsInverted;

    public float timeScale = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsInverted)
        {
            rb.velocity = -rb.velocity;
        }
        
        if (Utils.OutOfShip(transform))
        {
            OnStopWalking?.Invoke();

            rb.velocity += movSpeedMultiplier * outOfShipMoveForce * timeScale * moveInput + moveSpeedAddition;

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveSpeed * movSpeedMultiplier * timeScale);

            LoopBoundaryConditions();
        }
        else
        {
            if (moveInput != Vector2.zero)
            {
                rb.velocity = moveSpeed * movSpeedMultiplier * timeScale * moveInput;

                rb.velocity += movSpeedMultiplier * timeScale * moveSpeedAddition;

                OnStartWalking?.Invoke();
            }
            else
            {
                rb.velocity = Vector2.zero;

                rb.velocity += moveSpeedAddition * movSpeedMultiplier;

                OnStopWalking?.Invoke();
            }
        }

        if (controlsInverted)
        {
            rb.velocity = -rb.velocity;
        }
    }

    private void LoopBoundaryConditions()
    {
        if (Mathf.Abs(transform.position.x) > distToLoopToOtherSide.x)
        {
            transform.position = new Vector3(-transform.position.x * 0.95f, transform.position.y, transform.position.z);
            OnLoopBoundaries?.Invoke();
        }
        if (Mathf.Abs(transform.position.y) > distToLoopToOtherSide.y)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y * 0.95f, transform.position.z);
            OnLoopBoundaries?.Invoke();
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

    public void SetAddVelocity(Vector2 velocity)
    {
        moveSpeedAddition = timeScale * velocity;
    }

    public void SetControlsInverted(bool inverted)
    {
        controlsInverted = inverted;
    }

    internal void StopMovement()
    {
        rb.velocity = Vector2.zero;
        OnStopWalking?.Invoke();
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
