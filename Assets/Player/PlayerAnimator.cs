using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;
    
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = frontSprite;
    }

    private void Update()
    {
        if(rb.velocity != Vector2.zero) spriteRenderer.sprite = GetSpriteFromVelocity();
    }

    Sprite GetSpriteFromVelocity()
    {
        if (Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
        {
            if (rb.velocity.y > 0)
            {
                return backSprite;
            }
            else
            {
                return frontSprite;
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                return rightSprite;
            }
            else
            {
                return leftSprite;
            }
        }
    }
}