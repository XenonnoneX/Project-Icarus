using UnityEngine;

public class ShowBroken : MonoBehaviour
{
    [SerializeField] Interactable interactable;
    ControlStation station;
    [SerializeField] Light2D warningLight;

    [SerializeField] SpriteRenderer brokenSprite;
    [SerializeField] Sprite brokenSprite0, brokenSprite1, brokenSprite2;

    private void Awake()
    {
        station = interactable.station;
    }

    private void Update()
    {
        float brokenPercentage = station.GetBrokenTimePercentage();

        warningLight.SetBlinkSpeedMultiplier(brokenPercentage);
        warningLight.SetAlphaMulitplier(brokenPercentage);
        warningLight.SetTimeScale(station.timeScale);

        if (brokenPercentage < 0.33f)
        {
            brokenSprite.sprite = brokenSprite0;
        }
        else if (brokenPercentage < 0.66f)
        {
            brokenSprite.sprite = brokenSprite1;
        }
        else
        {
            brokenSprite.sprite = brokenSprite2;
        }
    }

}