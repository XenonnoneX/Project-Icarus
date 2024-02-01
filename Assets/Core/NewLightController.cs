using UnityEngine;

public class NewLightController : MonoBehaviour
{
    [SerializeField] Light2D light2D;
    [SerializeField] FieldOfVision newLight;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Update()
    {
        newLight.lightColor = light2D.LightColor;
        spriteRenderer.color = light2D.LightColor;
    }
}