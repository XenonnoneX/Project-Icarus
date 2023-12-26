using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BlackHoleEffect : MonoBehaviour
{
    [SerializeField] Shader shader;
    [SerializeField] Transform blackHole;
    float ratio = 0.5625f; // aspect ratio of the screen
    [SerializeField] float radius = 1;
    [SerializeField] float strength = 1;

    Camera cam;
    Material _material; // will be procedurally generated

    Vector3 wtsp;
    Vector2 pos;

    Material material
    {
        get
        {
            if (_material == null)
            {
                _material = new Material(shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }

            return _material;
        }
    }
    void OnEnable()
    {
        cam = GetComponent<Camera>();

        ratio = 1f / cam.aspect;

        //cam.depthTextureMode = DepthTextureMode.Depth;
    }

    void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shader && material && blackHole)
        {
            wtsp = cam.WorldToScreenPoint(blackHole.position);

            if(wtsp.z > 0)
            {
                pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);

                _material.SetVector("_Position", pos);
                material.SetFloat("_Ratio", ratio);
                _material.SetFloat("_Rad", radius);
                _material.SetFloat("_Distance", Vector3.Distance(blackHole.position, transform.position));
                _material.SetFloat("_Strength", strength);

                Graphics.Blit(source, destination, material);
            }
        }
    }
}
