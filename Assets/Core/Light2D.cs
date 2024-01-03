using System;
using UnityEngine;

public class Light2D : MonoBehaviour, TimeAffected
{
    [SerializeField] LayerMask wallLayer = 1 << 10;
    [SerializeField] float lightRange = 1f;
    [SerializeField] Color lightColor = Color.white;
    [SerializeField] float blinkingSpeed = 10f; // Blinks per second
    float blinkSpeedMultiplier = 1;
    float alphaMultiplier = 1;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    [SerializeField] int sortingLayer = 15;

    float timeScale = 1;
    float totalTime = 0;

    private void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        GenerateMesh();
        UpdateMeshColor();

        // Create a URP material with the 2D Lit shader
        Material urpMaterial = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));
        meshRenderer.material = urpMaterial;

        // Set the sorting order
        meshRenderer.sortingLayerName = "Default"; // Change to your desired sorting layer
        meshRenderer.sortingOrder = sortingLayer; // Change to your desired sorting order
    }

    private void Update()
    {
        totalTime += Time.deltaTime * timeScale;

        GenerateMesh();
        // Update the mesh color based on the current state of the light
        UpdateMeshColor();
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector2 origin = transform.position;
        int rayCount = 360;
        float angleIncrement = 360f / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1];
        Color[] colors = new Color[rayCount + 1];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;
        colors[0] = lightColor;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleIncrement;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, lightRange, wallLayer);

            float distance = hit ? hit.distance : lightRange;

            vertices[vertexIndex] = direction * distance;
            float normalizedDistance = distance / lightRange;
            float intensity = 1f - Mathf.Pow(distance / lightRange, 2f);
            colors[vertexIndex] = lightColor * intensity;

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

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
    }


    private void UpdateMeshColor()
    {
        float currentBlinkingSpeed = blinkSpeedMultiplier * blinkingSpeed;

        float alpha = Mathf.Cos(totalTime * currentBlinkingSpeed) * 0.5f + 0.5f;
        lightColor.a = alpha * alphaMultiplier;
    }

    internal void SetBlinkSpeedMultiplier(float v)
    {
        blinkSpeedMultiplier = v;
    }

    public void SetAlphaMulitplier(float multiplier)
    {
        alphaMultiplier = multiplier;
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}
