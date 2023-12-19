using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RepairTask : Task
{   
    [SerializeField] Slider slider;
    [SerializeField] float holdDownTime = 3f;

    float repairTime;

    protected override void Start()
    {
        base.Start();
    }

    protected override void UpdateLogic()
    {
        base.UpdateLogic();

        repairTime += controls.Player.Repair.ReadValue<float>() * Time.deltaTime;

        slider.value = repairTime / holdDownTime;

        if (repairTime >= holdDownTime)
        {
            interactable.FixInteractable();
            EndTask();
        }
    }

    public override void StartTask(Interactable interactable)
    {
        print("start repair task");
        base.StartTask(interactable);
        
        repairTime = 0f;
        slider.value = 0f;
    }
}
