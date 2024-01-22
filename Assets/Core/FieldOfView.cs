using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfVision : MonoBehaviour
{
    public float lightRange = 10f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask wallLayer;
    public Material visibleAreaMaterial;
    public Color lightColor = Color.green;

    [SerializeField] int sortingLayer = 16;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // meshFilter = gameObject.AddComponent<MeshFilter>();
        // meshFilter.mesh = new Mesh();
        // meshFilter.mesh.name = "VisibleAreaMesh";
        // 
        // meshRenderer = gameObject.AddComponent<MeshRenderer>();
        // meshRenderer.material = visibleAreaMaterial;

        spriteRenderer.sortingOrder = sortingLayer;
        spriteRenderer.material.color = lightColor;


        // Set the emission color
        spriteRenderer.material.SetColor("_EmissionColor", lightColor);
        spriteRenderer.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        // UpdateVisibleAreaMesh();

        spriteRenderer.material.SetColor("_EmissionColor", lightColor);
        spriteRenderer.color = lightColor;
    }

    void UpdateVisibleAreaMesh()
    {
        int rayCount = 100;
        float angleStep = viewAngle / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1];
        int[] triangles = new int[rayCount * 3];

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + angleStep * i;
            Vector2 dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, lightRange, wallLayer);
            
            Debug.DrawLine(transform.position, hit.point, Color.red);
                
            float distance = hit ? hit.distance : lightRange;

            vertices[vertexIndex] = dir * distance;

            if (i < rayCount - 1)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex;
                triangles[triangleIndex + 2] = vertexIndex + 1;
            }
            else
            {
                // Connect the last triangle to the first vertex
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex;
                triangles[triangleIndex + 2] = 1;
            }

            triangleIndex += 3;
            vertexIndex++;
        }

        Mesh mesh = meshFilter.mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
