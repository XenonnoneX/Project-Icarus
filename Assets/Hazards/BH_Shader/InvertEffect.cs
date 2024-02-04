using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InvertEffect : MonoBehaviour
{
    [SerializeField] Shader shader;
    float ratio = 0.5625f; // aspect ratio of the screen
    [SerializeField] float timeMultiplier = 10f;
    [SerializeField] float strength = 0.1f;
    [SerializeField] float frequency = 20f;
    [SerializeField] float saturationAdd = 0.2f;
    [SerializeField] float saturationSinMultiplier = .3f;
    [SerializeField] Vector2 dir;

    Camera cam;
    Material _material; // will be procedurally generated

    float effectTime = 0f;
    float effectDuration = Mathf.Infinity;
    [SerializeField] float effectAnimateDuration = Mathf.Infinity;

    [SerializeField] bool effectActive;

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
        
        if(effectTime > effectDuration)
        {
            EndEffect();
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
        if (shader && material && effectActive)
        {
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    internal void StartEffect(float effectDuration)
    {        
        effectTime = 0;
        this.effectDuration = effectDuration;

        effectActive = true;
    }

    public void EndEffect()
    {
        effectActive = false;
    }
}


/*
 * using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Camera))]
public class GravityPullEffect : MonoBehaviour
{
    const int MAX_EFFECTS = 5;

    public Shader shader;
    public Transform[] pullEffectObjectTransforms = new Transform[MAX_EFFECTS];
    public float ratio; // aspect ratio of the screen
    public float radius;
    public float strength;

    Camera cam;
    Material _material; // will be procedurally generated

    Vector3 wtsp;
    public Vector4[] positions = new Vector4[MAX_EFFECTS];
    float[] effectTimes = new float[MAX_EFFECTS];
    float[] effectDurations = new float[MAX_EFFECTS];
    
    [SerializeField] float effectAnimateDuration = 2f;

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
        for (int i = 0; i < effectTimes.Length; i++)
        {
            if (effectTimes[i] < effectAnimateDuration)
            {
                effectTimes[i] += Time.deltaTime;
            }
            else if (effectTimes[i] > effectDurations[i])
            {
                StopEffect(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                effectTimes[i] = 0;
            }
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
        if (shader && material && AnyEffectActive())
        {
            float[] distances = new float[MAX_EFFECTS];
            float[] timesSinceStart = new float[MAX_EFFECTS];

            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                if (pullEffectObjectTransforms[i] == null)
                {
                    distances[i] = -1;
                    timesSinceStart[i] = -1;
                }
                else
                {
                    distances[i] = Vector3.Distance(pullEffectObjectTransforms[i].position, transform.position);
                    timesSinceStart[i] = effectTimes[i] * strength;
                }
            }

            _material.SetVectorArray("_Positions", positions);
            _material.SetFloat("_Ratio", ratio);
            _material.SetFloat("_Rad", radius);
            _material.SetFloatArray("_Distances", distances);
            _material.SetFloatArray("_TimesSinceStart", timesSinceStart);

            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private bool AnyEffectActive()
    {
        for (int i = 0; i < MAX_EFFECTS; i++)
        {
            if (pullEffectObjectTransforms[i] != null) return true;
        }
        return false;
    }

    public void StartEffect(Transform pullObjectTransform, float effectDuration)
    {
        for (int i = 0; i < MAX_EFFECTS; i++)
        {
            if (pullEffectObjectTransforms[i] == null)
            {
                pullEffectObjectTransforms[i] = pullObjectTransform;

                effectTimes[i] = 0;
                effectDurations[i] = effectDuration;

                wtsp = cam.WorldToScreenPoint(pullObjectTransform.position);
                positions[i] = new Vector2(wtsp.x / cam.pixelWidth, wtsp.y / cam.pixelHeight);

                return;
            }
        }
    }

    internal void StopEffect(int index)
    {
        effectTimes[index] = 0;
        pullEffectObjectTransforms[index] = null;
        positions[index] = Vector4.zero;
    }
}

 * 
 */