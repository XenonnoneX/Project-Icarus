using System;
using System.Collections.Generic;

public class GetArtifactTask : Task
{
    ArtifactManager artifactManager;
    ArtifactDock artifactDock;
    MissionManager missionManager;
    public List<Artifact> chooseableArtifacts = new List<Artifact>();

    public delegate void OnGetChoice();
    public OnGetChoice onGetChoice;

    private void Awake()
    {
        artifactManager = FindObjectOfType<ArtifactManager>();
        artifactDock = FindObjectOfType<ArtifactDock>();
        missionManager = FindObjectOfType<MissionManager>();

        missionManager.onMissionCompleted += CheckGetArtifact;
    }

    private void CheckGetArtifact()
    {
        print("CheckGetArtifact");

        if(missionManager.CompletedMissionsCount % missionManager.missionsForArtifact == 0)
        {
            artifactDock.availableArtifactsCount++;
            StartTask(null);
        }
    }

    public override void StartTask(Interactable interactable)
    {
        base.StartTask(interactable);

        TimeManager.instance.Pause();

        if (chooseableArtifacts.Count == 0) GetArtifactChoice();
    }

    public override void EndTask()
    {
        TimeManager.instance.Unpause();
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
