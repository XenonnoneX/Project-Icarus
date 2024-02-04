using System.Collections.Generic;
using UnityEngine;

public class StringOfTimeVisuals : MonoBehaviour
{
    LineRenderer lineRenderer;
    StringOfTimeAbility stringOfTimeAbility;
    PlayerAnimator playerAnimator;
    [SerializeField] GameObject playerVisuals;
    Animator shadowAnimator;
    GameObject playerShadow;

    [SerializeField] float wiggleAmount = 0.05f;

    Queue<Vector3> positionQueue = new Queue<Vector3>();
    Queue<bool> isWalkingQueue = new Queue<bool>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        stringOfTimeAbility = GetComponent<StringOfTimeAbility>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();

        stringOfTimeAbility.onPositionSaved += DrawLine;
    }

    private void Start()
    {
        // copy player visuals and spawn them at the first position

        playerShadow = Instantiate(playerVisuals, transform);

        shadowAnimator = playerShadow.GetComponentInChildren<Animator>();

        SpriteRenderer[] playerSpriteRenderers = playerShadow.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in playerSpriteRenderers)
        {
            renderer.color = new Color(0, 0, 0, 0.5f);
        }
    }

    void Update()
    {
        if (stringOfTimeAbility.TimeSinceLastUse < stringOfTimeAbility.Cooldown)
        {
            lineRenderer.enabled = false;
            playerShadow.SetActive(false);

            positionQueue.Clear();
            isWalkingQueue.Clear();
        }
        else
        {
            lineRenderer.enabled = true;
        }
    }
     
    private void DrawLine()
    {
        Vector3[] positions = stringOfTimeAbility.positions.ToArray();

        positionQueue.Enqueue(positions[stringOfTimeAbility.positions.Count - 1] + new Vector3(Random.Range(-wiggleAmount, wiggleAmount), Random.Range(-wiggleAmount, wiggleAmount), 0));
        isWalkingQueue.Enqueue(playerAnimator.isWalking);

        if (positionQueue.Count > positions.Length)
        {
            positionQueue.Dequeue();
            isWalkingQueue.Dequeue();
        }

        lineRenderer.positionCount = positions.Length;

        lineRenderer.SetPositions(positionQueue.ToArray());

        playerShadow.SetActive(true);
        
        playerShadow.transform.position = positions[0];
        shadowAnimator.SetBool("isWalking", isWalkingQueue.Peek());
    }
}