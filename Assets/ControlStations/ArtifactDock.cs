using System.Collections.Generic;
using UnityEngine;

public class ArtifactDock : ControlStation{

    ArtifactManager artifactManager;

    public int artifactChooseAmount = 3;

    public int availableArtifactsCount;

    private void Awake()
    {
        artifactManager = FindObjectOfType<ArtifactManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.P)) availableArtifactsCount++;
    }

    public List<Artifact> GetRandomArtifacts()
    {
        List<Artifact> availableArtifacts = artifactManager.GetAvailableArtifacts();

        List<Artifact> artifacts = new List<Artifact>();

        int count= 0;
        while (artifacts.Count < artifactChooseAmount && artifacts.Count < availableArtifacts.Count)
        {
            int rand = Random.Range(0, availableArtifacts.Count);

            print(rand);

            Artifact artifact = availableArtifacts[rand];

            if (!artifacts.Contains(artifact))
            {
                artifacts.Add(artifact);
            }
            count++;
            if(count > 100)
            {
                print("break");
                print("available: " +availableArtifacts.Count);
                print("chosen: " + artifacts.Count);
                break;
            }
        }

        return artifacts;
    }
}