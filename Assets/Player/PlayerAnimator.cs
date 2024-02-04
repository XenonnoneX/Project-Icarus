using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement player;

    [SerializeField] Animator frontAnimator;
    [SerializeField] Animator rightAnimator;
    [SerializeField] Animator backAnimator;
    [SerializeField] Animator leftAnimator;

    Animator currentAnimator;

    Rigidbody2D rb;

    public bool isWalking = false;


    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        player.OnStartWalking += StartWalking;
        player.OnStopWalking += StopWalking;
    }

    private void Start()
    {
        rightAnimator.gameObject.SetActive(false);
        backAnimator.gameObject.SetActive(false);
        leftAnimator.gameObject.SetActive(false);

        SetAnimatorActive(frontAnimator);
    }

    private void Update()
    {
        if (rb.velocity != Vector2.zero) SetAnimatorActive(GetAnimatorFromVelocity());
    }

    private void StopWalking()
    {
        SetIsWalking(false);
    }

    private void StartWalking()
    {
        SetIsWalking(true);
    }

    public void SetIsWalking(bool value)
    {
        isWalking = value;
        frontAnimator.SetBool("isWalking", value);
        rightAnimator.SetBool("isWalking", value);
        backAnimator.SetBool("isWalking", value);
        leftAnimator.SetBool("isWalking", value);
    }

    void SetAnimatorActive(Animator animator)
    {
        if(animator == currentAnimator) return;

        if(currentAnimator != null) currentAnimator.gameObject.SetActive(false);

        currentAnimator = animator;

        currentAnimator.gameObject.SetActive(true);
    }


    Animator GetAnimatorFromVelocity()
    {
        if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
        {
            if (rb.velocity.y > 0)
            {
                return backAnimator;
            }
            else
            {
                return frontAnimator;
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                return rightAnimator;
            }
            else
            {
                return leftAnimator;
            }
        }
    }
}