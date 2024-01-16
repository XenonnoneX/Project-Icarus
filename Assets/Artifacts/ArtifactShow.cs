using System.Linq;
using System.Text;
using TMPro;
using UnityEditor.Experimental;
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

        UpdateNameText(artifact);
    }

    void UpdateNameText(Artifact artifact)
    {
        StringBuilder nameBuilder = new StringBuilder(artifact.artifactData.name);

        if (artifact.currentLevel != 0)
        {
            nameBuilder.Append(" I");

            for (int i = 0; i < artifact.currentLevel; i++)
            {
                nameBuilder.Append("I");
            }
        }

        nameText.text = nameBuilder.ToString();
    }
}