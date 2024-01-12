using System.Collections.Generic;
using UnityEngine;

public class StringOfTimeAbility : Artifact, TimeAffected, Ability
{
    Transform player;

    [SerializeField] float cooldown = 1f;
    [SerializeField] float abilityDuration = 5f;
    [SerializeField] float saveTime = 0.1f;

    public Queue<Vector3> positions = new Queue<Vector3>();

    float timeSinceLastUse = Mathf.Infinity;

    public delegate void OnPositionSaved();
    public event OnPositionSaved onPositionSaved;
    public event Ability.OnAbilityUsed onAbilityUsed;

    float timeScale = 1;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    protected override void EnableArtifact()
    {
        base.EnableArtifact();

        InvokeRepeating("SaveCurrentPosition", 0f, saveTime);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        timeSinceLastUse += Time.deltaTime * timeScale;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(timeSinceLastUse >= cooldown) UseAbility();
        }
    }

    private void SaveCurrentPosition()
    {
        positions.Enqueue(player.position);
        if (positions.Count > abilityDuration / saveTime)
        {
            positions.Dequeue();
        }
        onPositionSaved.Invoke();
    }

    void UseAbility()
    {
        player.position = positions.Dequeue();
        positions.Clear();

        timeSinceLastUse = 0f;

        onAbilityUsed?.Invoke();
    }

    public void SetTimeScale(float timeScale)
    {
        this.timeScale = timeScale;
    }
}
