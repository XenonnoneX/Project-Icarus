using System.Collections;
using System.Collections.Generic;

public class GetArtifactTask : Task
{
    ArtifactManager artifactManager;
    ArtifactDock artifactDock;
    public List<Artifact> chooseableArtifacts = new List<Artifact>();

    public delegate void OnGetChoice();
    public OnGetChoice onGetChoice;

    private void Awake()
    {
        artifactManager = FindObjectOfType<ArtifactManager>();
        artifactDock = FindObjectOfType<ArtifactDock>();
    }

    public override void StartTask(Interactable interactable)
    {
        base.StartTask(interactable);

        TimeManager.instance.SetTimeScale(0);

        if (chooseableArtifacts.Count == 0) GetArtifactChoice();
    }

    public override void EndTask()
    {
        TimeManager.instance.SetTimeScale(1);
        base.EndTask();
    }

    void GetArtifactChoice()
    {
        if(artifactDock.availableArtifactsCount <= 0)
        {
            EndTask();
            return;
        }

        chooseableArtifacts = artifactDock.GetRandomArtifacts();

        onGetChoice?.Invoke();
    }

    public void SelectArtifact(int index)
    {
        artifactManager.AddOrLevelUpArtifact(chooseableArtifacts[index]);
        artifactDock.availableArtifactsCount--;

        chooseableArtifacts.Clear();

        EndTask();
        StartTask(interactable);
    }
}
