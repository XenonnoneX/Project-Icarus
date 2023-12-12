using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GravityPullEffect : MonoBehaviour
{
    public Shader shader;
    public Transform pullEffectObject;
    public float ratio; // aspect ratio of the screen
    public float radius;
    public float strength;

    Camera cam;
    Material _material; // will be procedurally generated

    Vector3 wtsp;
    Vector2 pos;

    public bool effectActive = false;
    float effectTime = 0f;
    [SerializeField] float effectDuration = 2f;

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

    private void Update()
    {

        if (effectTime < effectDuration)
        {
            effectTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            effectTime = 0;
            effectActive = !effectActive;
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
        if (effectActive && shader && material && pullEffectObject)
        {
            wtsp = cam.WorldToScreenPoint(pullEffectObject.position);

            if (wtsp.z > 0)
            {
                pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);

                _material.SetVector("_Position", pos);
                material.SetFloat("_Ratio", ratio);
                _material.SetFloat("_Rad", radius);
                _material.SetFloat("_Distance", Vector3.Distance(pullEffectObject.position, transform.position));
                _material.SetFloat("_TimeSinceStart", effectTime * strength);

                Graphics.Blit(source, destination, material);
            }
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
