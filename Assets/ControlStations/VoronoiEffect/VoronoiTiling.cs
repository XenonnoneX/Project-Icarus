using System.Collections.Generic;
using UnityEngine;

public class VoronoiTiling : MonoBehaviour
{
    [SerializeField] Material voronoiMaterial;
    [SerializeField] Color brokenColor;
    [SerializeField] Color destroyedColor;

    [SerializeField] List<Interactable> stationInteractables;
    // Start is called before the first frame update
    void Start()
    {
        Voronoi();
    }

    private void Update()
    {
        Voronoi();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Voronoi();
        }
    }

    private void Voronoi()
    {
        List<Vector2> convertedPositions = GetStationPositions();

        voronoiMaterial.SetInt("_StationCount", stationInteractables.Count);

        voronoiMaterial.SetVectorArray("_Positions", Vector2ToVector4(convertedPositions));

        voronoiMaterial.SetColorArray("_Colors", GetStationColors());


        // Create a temporary RenderTexture
        RenderTexture tempRT = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.Default);
        tempRT.filterMode = FilterMode.Bilinear;

        // Blit the current screen contents to the temporary RenderTexture
        Graphics.Blit(null, tempRT, voronoiMaterial);

        // Blit the temporary RenderTexture to the screen
        Graphics.Blit(tempRT, (RenderTexture)null);

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tempRT);
    }

    List<Color> GetStationColors()
    {
        List<Color> colors = new List<Color>();

        for (int i = 0; i < stationInteractables.Count; i++)
        {
            if (stationInteractables[i].station.GetStationState() == StationState.Broken)
                colors.Add(brokenColor);
            else if (stationInteractables[i].station.GetStationState() == StationState.Destroyed)
                colors.Add(destroyedColor);
            else
                colors.Add(stationInteractables[i].station.stationColor);
        }

        return colors;
    }

    private List<Vector2> GetStationPositions()
    {
        List<Vector2> positions = new List<Vector2>();

        for (int i = 0; i < stationInteractables.Count; i++)
        {
            positions.Add(Camera.main.WorldToScreenPoint(stationInteractables[i].transform.position));
        }

        return positions;
    }

    List<Vector4> Vector2ToVector4(List<Vector2> vector2s)
    {
        List<Vector4> vector4s = new List<Vector4>();

        for (int i = 0; i < vector2s.Count; i++)
        {
            vector4s.Add(new Vector4(vector2s[i].x, vector2s[i].y, 0, 1));
        }

        return vector4s;
    }
}
