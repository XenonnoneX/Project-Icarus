using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.velocity.x > 0.3f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x < 0.3f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}