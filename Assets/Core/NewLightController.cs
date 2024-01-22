using UnityEngine;

public class NewLightController : MonoBehaviour
{
    [SerializeField] Light2D light2D;
    [SerializeField] FieldOfVision newLight;

    private void Update()
    {
        newLight.lightColor = light2D.LightColor;
    }
}