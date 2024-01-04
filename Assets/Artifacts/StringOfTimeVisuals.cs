using UnityEngine;

public class StringOfTimeVisuals : MonoBehaviour
{
    LineRenderer lineRenderer;
    StringOfTimeAbility stringOfTimeAbility;
    [SerializeField] GameObject playerVisuals;
    GameObject playerShadow;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        stringOfTimeAbility = GetComponent<StringOfTimeAbility>();

        stringOfTimeAbility.onPositionSaved += DrawLine;
    }

    private void Start()
    {
        // copy player visuals and spawn them at the first position

        playerShadow = Instantiate(playerVisuals, transform);
        playerShadow.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
    }

    private void DrawLine()
    {
        Vector3[] positions = stringOfTimeAbility.positions.ToArray();

        lineRenderer.positionCount = positions.Length;

        lineRenderer.SetPositions(positions);

        playerShadow.transform.position = positions[0];
    }
}