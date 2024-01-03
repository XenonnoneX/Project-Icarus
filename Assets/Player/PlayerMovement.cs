using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, TimeAffected
{
    Rigidbody2D rb;
    Vector2 moveInput;
    public Vector2 MoveInput { get => moveInput; }

    [SerializeField] float moveSpeed = 5f;
    [HideInInspector] public float movSpeedMultiplier = 1;

    public float timeScale = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
     //   print("Hello Im the Movement");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * moveSpeed * movSpeedMultiplier * timeScale;
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

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}
