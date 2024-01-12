using System.Collections.Generic;
using UnityEngine;
 
public class GetArtifactTaskUI : MonoBehaviour
{
    ArtifactDock artifactDock;
    GetArtifactTask getArtifactTask;
    
    [SerializeField] ArtifactShow artifactShowPrefab;
    [SerializeField] GameObject artifactParent;

    List<ArtifactShow> artifactShowObjects = new List<ArtifactShow>();

    private void Awake()
    {
        artifactDock = FindObjectOfType<ArtifactDock>();
        getArtifactTask = FindObjectOfType<GetArtifactTask>();
        getArtifactTask.onGetChoice += UpdateUI;
        getArtifactTask.onInteractEnd += UpdateUI;
    }

    private void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        SpawnUpgradeShowObjects();

        SetUpgrades();
    }

    private void SetUpgrades()
    {
        for (int i = 0; i < artifactShowObjects.Count; i++)
        {
            if (getArtifactTask.chooseableArtifacts.Count > i)
            {
                artifactShowObjects[i].SetArtifact(getArtifactTask.chooseableArtifacts[i]);
                artifactShowObjects[i].gameObject.SetActive(true);
            }
            else
            {
                artifactShowObjects[i].gameObject.SetActive(false);
            }

        }
    }

    private void SpawnUpgradeShowObjects()
    {   
        if (artifactShowObjects.Count != 0) return;

        for (int i = 0; i < artifactDock.artifactChooseAmount; i++)
        {
            int index = i;
            ArtifactShow temp = Instantiate(artifactShowPrefab, artifactParent.transform);
            artifactShowObjects.Add(temp);
            temp.selectButton.onClick.AddListener(() => SelectArtifact(index));
        }
    }

    private void SelectArtifact(int i)
    {
        getArtifactTask.SelectArtifact(i);
    }
}
