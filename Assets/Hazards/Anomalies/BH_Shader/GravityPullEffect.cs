using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GravityPullEffect : MonoBehaviour
{
    public Shader shader;
    public Transform pullEffectObject;
    List<Transform> nextPullEffectObjects = new List<Transform>();
    public float ratio; // aspect ratio of the screen
    public float radius;
    public float strength;

    Camera cam;
    Material _material; // will be procedurally generated

    Vector3 wtsp;
    Vector2 pos;

    float effectTime = 0f;
    [SerializeField] float effectDuration = 10f;
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
        
        if(effectTime > effectDuration)
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

                _material.SetVector("_Position", pos);
                material.SetFloat("_Ratio", ratio);
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

    internal void StartEffect(Transform pullObjectTransform)
    {
        if (pullEffectObject != null) EndEffect();

        pullEffectObject = pullObjectTransform;

        effectTime = 0;

        effectActive = true;
    }

    private void EndEffect()
    {
        Destroy(pullEffectObject.gameObject);
        pullEffectObject = null;

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