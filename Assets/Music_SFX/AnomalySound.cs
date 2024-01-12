using UnityEngine;

[RequireComponent(typeof(Anomaly))]
public class AnomalySound : MonoBehaviour
{
    Anomaly anomaly;

    [SerializeField] AudioClip anomalySpawnSound;
    [SerializeField] AudioClip anomalyRemoveSound;

    private void Awake()
    {
        anomaly = GetComponent<Anomaly>();
        anomaly.onRemoveAnomaly += PlayRemoveSound;
    }

    protected virtual void Start()
    {
        SoundManager.instance.PlaySound(anomalySpawnSound);
    }

    private void PlayRemoveSound()
    {
        SoundManager.instance.PlaySound(anomalyRemoveSound);
    }
}
