using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentArtifactsUI : MonoBehaviour
{
    ArtifactManager artifactManager;
    [SerializeField] Transform artifactIconParent;
    [SerializeField] Image artifactIconPrefab;
    
    private void Awake()
    {
        artifactManager = FindObjectOfType<ArtifactManager>();
        artifactManager.onArtifactsChanged += UpdateUI;
    }

    void UpdateUI()
    {
        foreach (Transform child in artifactIconParent)
        {
            Destroy(child.gameObject);
        }

        List<Artifact> allArtifacts = artifactManager.allArtifacts;

        for (int i = 0; i < allArtifacts.Count; i++)
        {
            if (allArtifacts[i].currentLevel == 0) continue;

            Image artifactIcon = Instantiate(artifactIconPrefab, artifactIconParent);
            artifactIcon.sprite = allArtifacts[i].artifactData.sprite;
        }
    }
}
