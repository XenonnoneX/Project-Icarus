public class CatArtifact : Artifact
{
    CatInteractable catInteractable;

    protected override void Awake()
    {
        base.Awake();
        catInteractable = FindObjectOfType<CatInteractable>();
        catInteractable.gameObject.SetActive(false);
    }

    protected override void EnableArtifact()
    {
        base.EnableArtifact();
        catInteractable.gameObject.SetActive(true);
    }
}