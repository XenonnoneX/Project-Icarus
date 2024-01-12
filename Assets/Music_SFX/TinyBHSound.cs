using UnityEngine;

[RequireComponent(typeof(TinyBH))]
public class TinyBHSound : AnomalySound
{
    TinyBH tinyBH;

    [SerializeField] AudioClip hitPlayerSound;

    private void Awake()
    {
        tinyBH = GetComponent<TinyBH>();
        tinyBH.onHitPlayer += PlayHitPlayerSound;
    }

    private void PlayHitPlayerSound()
    {
        SoundManager.instance.PlaySound(hitPlayerSound);
    }
}