using System;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    ArtifactManager artifactManager;
    public ArtifactData artifactData;
    public int currentLevel;

    protected virtual void Awake()
    {
        artifactManager = FindObjectOfType<ArtifactManager>();
        artifactManager.AddArtifact(this);
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (currentLevel == 0) return;
    }

    protected virtual void EnableArtifact()
    {
        gameObject.SetActive(true);
    }

    public void LevelUp()
    {
        SetLevel(currentLevel + 1);
    }

    protected virtual void SetLevel(int level)
    {
        currentLevel = level;

        if (currentLevel != 0) EnableArtifact();
    }

    public bool IsMaxed()
    {
        return currentLevel >= artifactData.levelValues.Count;
    }

    public float GetLevelValue()
    {
        return artifactData.levelValues[currentLevel];
    }
}
