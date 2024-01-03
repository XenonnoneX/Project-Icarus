using UnityEngine;

public class TinyBH : Anomaly, TimeAffected
{
    [SerializeField] float movSpeed = 2f;

    Vector3 movDir;

    float timeScale = 1;

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }

    protected override void Start()
    {
        base.Start();
        anomalyType = AnomalyType.TinyBH;

        Camera.main.GetComponent<BlackHoleEffect>().StartEffect(this);

        SpawnSetup();
    }

    private void Update()
    {
        transform.position += movDir * movSpeed * Time.deltaTime * timeScale;
    }

    private void SpawnSetup()
    {
        transform.position = Utils.GetPositionOutOfScreen(2f);

        Vector3 dir = - transform.position;

        movDir = Utils.RotateVectorBy(UnityEngine.Random.Range(-30f, 30f), dir).normalized;
    }

    internal override void RemoveAnomaly()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = Utils.GetRandomPosOnWalkableArea();
        }
        else if(collision.GetComponent<Interactable>())
        {
            collision.GetComponent<Interactable>()?.BreakInteractable();
        }
    }
}