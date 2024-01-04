using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifact")]
public class ArtifactData : ScriptableObject
{
    public Sprite sprite;
    public ArtifactData requiredArtifact;
    public List<float> levelValues;
}