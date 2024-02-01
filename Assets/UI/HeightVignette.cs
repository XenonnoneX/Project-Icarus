using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightVignette : MonoBehaviour
{
    SpaceShipMovement shipMovement;
    SpriteRenderer spriteRenderer;

    Color vignetteColor;

    private void Awake()
    {
        shipMovement = FindObjectOfType<SpaceShipMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        vignetteColor = spriteRenderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shipMovement.HeightBelowWarningHeight())
        {
            vignetteColor.a = 1 - shipMovement.GetCurrentHeight() / shipMovement.GetWarnHeight();
        }
        else
        {
            vignetteColor.a = 0;
        }

        spriteRenderer.color = vignetteColor;
    }
}
