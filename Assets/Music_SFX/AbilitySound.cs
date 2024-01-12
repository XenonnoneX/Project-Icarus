using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilitySound : MonoBehaviour
{
    Ability ability;

    [SerializeField] AudioClip useAbilitySound;

    private void Awake()
    {
        ability = GetComponent<Ability>();
        ability.onAbilityUsed += PlayAbilitySound;
    }

    private void PlayAbilitySound()
    {
        SoundManager.instance.PlaySound(useAbilitySound);
    }
}