using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeVisuals : MonoBehaviour
{
    [SerializeField] SpriteRenderer lensRenderer;

    Telescope telescope;

    private void Start()
    {
        telescope = FindObjectOfType<Telescope>();
        telescope.onLensChanged += OnLensChanged;
    }

    private void OnLensChanged(ItemData currentLens)
    {
        if (currentLens == null) lensRenderer.sprite = null;
        else lensRenderer.sprite = currentLens.secondarySprite;
    }
}
