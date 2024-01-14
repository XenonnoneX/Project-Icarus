using UnityEngine;
using UnityEngine.UI;

public class RepairTask : Task
{   
    [SerializeField] Slider slider;
    [SerializeField] float holdDownTime = 3f;
    [HideInInspector] public float repairSpeedMultiplier = 1f;

    float repairTime;

    protected override void Start()
    {
        base.Start();
    }

    protected override void UpdateLogic()
    {
        base.UpdateLogic();

        repairTime += controls.Player.Repair.ReadValue<float>() * repairSpeedMultiplier * Time.deltaTime * timeScale;

        slider.value = repairTime / holdDownTime;

        if (repairTime >= holdDownTime)
        {
            interactable.FixInteractable();
            EndTask();
        }
    }

    public override void StartTask(Interactable interactable)
    {
        base.StartTask(interactable);

        interactable.StartRepair();


        repairTime = 0f;
        slider.value = 0f;
    }
}
