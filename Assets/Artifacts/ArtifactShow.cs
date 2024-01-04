using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactShow : MonoBehaviour
{
    public Button selectButton;
    [SerializeField] Image spriteImage;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;

    public void SetArtifact(Artifact artifact)
    {
        spriteImage.sprite = artifact.artifactData.sprite;
        levelText.text = "Level " + artifact.currentLevel.ToString() + "/ " + (artifact.artifactData.levelValues.Count-1).ToString();
        nameText.text = artifact.artifactData.name;
    }
}