using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoBehaviour
{
    public List<ArtifactData> allArtifactDatas;
    public List<Artifact> allArtifacts;

    public event Action onArtifactsChanged;

    bool ArtifactIsMaxLevel(int index)
    {
        if (allArtifacts[index].currentLevel == allArtifacts[index].artifactData.levelValues.Count - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal List<Artifact> GetNotMaxedArtifacts()
    {
        List<Artifact> notMaxedArtifacts = new List<Artifact>();

        for (int i = 0; i < allArtifacts.Count; i++)
        {
            if (!ArtifactIsMaxLevel(i))
            {
                notMaxedArtifacts.Add(allArtifacts[i]);
            }
        }
        return notMaxedArtifacts;
    }

    public List<Artifact> GetAvailableArtifacts()
    {
        List<Artifact> artifacts = GetNotMaxedArtifacts();

        for (int i = artifacts.Count - 1; i >= 0; i--)
        {
            if (artifacts[i].artifactData.requiredArtifact == null) continue;

            for (int j = 0; j < allArtifacts.Count; j++)
            {
                if (allArtifacts[j].artifactData == artifacts[i].artifactData.requiredArtifact)
                {
                    if (allArtifacts[j].currentLevel == 0)
                    {
                        artifacts.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        return artifacts;
    }

    internal void AddOrLevelUpArtifact(Artifact artifact)
    {
        allArtifacts[allArtifacts.IndexOf(artifact)].LevelUp();
        onArtifactsChanged?.Invoke();
    }

    internal void AddArtifact(Artifact artifact)
    {
        allArtifacts.Add(artifact);
        allArtifactDatas.Add(artifact.artifactData);
    }
}
