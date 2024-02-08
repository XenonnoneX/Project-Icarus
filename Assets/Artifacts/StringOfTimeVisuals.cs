using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StringOfTimeVisuals : MonoBehaviour
{
    LineRenderer lineRenderer;
    StringOfTimeAbility stringOfTimeAbility;
    PlayerAnimator playerAnimator;
    [SerializeField] GameObject playerVisuals;
    PlayerVisual playerShadow;

    [SerializeField] float wiggleAmount = 0.05f;

    Queue<Vector3> positionQueue = new Queue<Vector3>();
    Queue<bool> isWalkingQueue = new Queue<bool>();
    Queue<Vector2> dirQueue = new Queue<Vector2>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        stringOfTimeAbility = GetComponent<StringOfTimeAbility>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();

        stringOfTimeAbility.onPositionSaved += DrawLine;
        stringOfTimeAbility.onAbilityUsed += FlyBackAnimation;
    }

    private void Start()
    {
        // copy player visuals and spawn them at the first position

        playerShadow = new PlayerVisual(Instantiate(playerVisuals, transform), new Color(0, 0, 0, 0.5f));
    }

    void Update()
    {
        if (stringOfTimeAbility.TimeSinceLastUse < stringOfTimeAbility.Cooldown)
        {
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
        dirQueue.Enqueue(playerAnimator.direction);

        if (positionQueue.Count > positions.Length)
        {
            positionQueue.Dequeue();
            isWalkingQueue.Dequeue();
            dirQueue.Dequeue();
        }

        lineRenderer.positionCount = positions.Length;

        lineRenderer.SetPositions(positionQueue.ToArray());

        playerShadow.gameObject.SetActive(true);
        
        playerShadow.gameObject.transform.position = positions[0];

        playerShadow.SetIsWalking(isWalkingQueue.Peek());

        playerShadow.SetVisualDirection(dirQueue.Peek());
    }

    private void FlyBackAnimation()
    {
        StartCoroutine(FlyBackRoutine());
        
    }

    IEnumerator FlyBackRoutine()
    {
        PlayerVisual playerVisual = new PlayerVisual(playerAnimator.gameObject, Color.white);
        Vector2[] directions = dirQueue.ToArray();
        bool[] isWalkings = isWalkingQueue.ToArray();
        
        
        for (float i = 0; i < stringOfTimeAbility.PlayerFlybackTime; i += Time.deltaTime)
        {
            int neededIndex = directions.Length - 1 - (int)(i / stringOfTimeAbility.PlayerFlybackTime * directions.Length);

            playerVisual.SetVisualDirection(directions[neededIndex]);
            playerVisual.SetIsWalking(isWalkings[neededIndex]);

            Vector3[] positions = positionQueue.ToArray();
            lineRenderer.positionCount = neededIndex;
            

            lineRenderer.SetPositions(positions);

            yield return null;
        }
        
        lineRenderer.enabled = false;
        playerShadow.gameObject.SetActive(false);

        positionQueue.Clear();
        isWalkingQueue.Clear();
        dirQueue.Clear();
    }
}