using System.Collections;
using UnityEngine;

public class Dash : Artifact, Ability
{
    protected PlayerMovement playerMovement;
    Rigidbody2D playerRB;

    [SerializeField] public AnimationCurve dashCurve;
    public float dashRange = 2f;
    [SerializeField] public float dashDuration = 0.2f;
    
    [SerializeField] public float cooldown = 0.5f;
    float timeSinceLastUse = Mathf.Infinity;

    public event Ability.OnAbilityUsed onAbilityUsed;

    protected override void Awake()
    {
        base.Awake();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerRB = playerMovement.GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();

        timeSinceLastUse += Time.deltaTime;
    }

    void OnDash()
    {
        print("Dash");
        if (timeSinceLastUse >= cooldown)
        {
            UseAbility();
            timeSinceLastUse = 0;
        }
    }

    void UseAbility()
    {
        onAbilityUsed?.Invoke();
        AbilityLogic();
    }

    protected virtual void AbilityLogic()
    {
        StartCoroutine(DoDash());
    }

    IEnumerator DoDash()
    {
        print("DoDash");
        playerRB.velocity = Vector2.zero;
        
        playerMovement.enabled = false;
        
        // Get dash direction and distance based on player input
        Vector2 dashDir = playerMovement.MoveInput;

        Vector2 dashVector = dashDir * GetDashRange(dashDir);

        // Calculate the destination point
        Vector2 dashDestination = (Vector2)playerMovement.transform.position + dashVector;

        // Check if the destination is valid (not colliding with any obstacles)

        // Smoothly move the player
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            float curveValue = dashCurve.Evaluate(t);
            
            playerMovement.transform.position = Vector2.Lerp(playerMovement.transform.position, dashDestination, curveValue);
                
            elapsedTime += Time.deltaTime * playerMovement.timeScale;
            yield return null;
        }
        
        playerMovement.enabled = true;
    }

    private float GetDashRange(Vector2 dashDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(playerMovement.transform.position, dashDir * dashRange, dashRange);

        if (hit.collider != null)
        {
            return Mathf.Max(0, hit.distance);
        }
        else
        {
            return dashRange;
        }
    }

    bool IsCollisionInPath(Vector2 start, Vector2 end)
    {
        // Cast a ray from start to end to check for collisions
        RaycastHit2D hit = Physics2D.Linecast(start, end);

        if (hit.collider != null) print("hit: " + hit.collider.name);

        // If hit.collider is not null, there's a collision
        return hit.collider != null;
    }
}