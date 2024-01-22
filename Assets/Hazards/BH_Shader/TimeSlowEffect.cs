using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TimeSlowEffect : MonoBehaviour
{
    [SerializeField] Shader shader;
    Transform pullEffectObject;
    List<Transform> nextPullEffectObjects = new List<Transform>();
    float ratio = 0.5625f; // aspect ratio of the screen
    public float radius = 4;
    [SerializeField] float strength = 0.1f;
    [SerializeField] float _SaturationAdd = -0.3f;

    Camera cam;
    Material _material; // will be procedurally generated

    Vector3 wtsp;
    Vector2 pos;

    float effectTime = 0f;
    float effectDuration = 8f;
    [SerializeField] float effectAnimateDuration = 2f;

    bool effectActive;

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
        if (!effectActive) return;

        effectTime += Time.deltaTime;

        if (effectTime > effectDuration)
        {
            EndEffect();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            effectTime = 0;
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
        if (shader && material && pullEffectObject)
        {
            wtsp = cam.WorldToScreenPoint(pullEffectObject.position);

            if (wtsp.z > 0)
            {
                pos = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);

                _material.SetFloat("_SaturationAdd", _SaturationAdd);
                _material.SetVector("_Position", pos);
                _material.SetFloat("_Ratio", ratio);
                _material.SetFloat("_Rad", radius);
                _material.SetFloat("_Distance", Vector3.Distance(pullEffectObject.position, transform.position));
                _material.SetFloat("_TimeSinceStart", Mathf.Min(effectTime, effectAnimateDuration) * strength);

                Graphics.Blit(source, destination, material);
            }
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    internal void StartEffect(Transform pullObjectTransform, float effectDuration)
    {
        if (pullEffectObject != null) EndEffect();

        pullEffectObject = pullObjectTransform;

        effectTime = 0;
        this.effectDuration = effectDuration;

        effectActive = true;
    }

    public void EndEffect()
    {
        if (pullEffectObject != null) Destroy(pullEffectObject.gameObject);
        pullEffectObject = null;

        effectActive = false;
    }
}
