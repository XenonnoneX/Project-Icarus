using UnityEngine.InputSystem;

public class ControlShipHeightTask : Task
{
    SpaceShipMovement spaceShipMovement;

    private void Awake()
    {
        spaceShipMovement = FindObjectOfType<SpaceShipMovement>();
    }
    
    protected override void UpdateLogic()
    {
        base.UpdateLogic();

        if (controls.Player.Interact.triggered)
        {
            spaceShipMovement.GoUp();
        }
        else if (controls.Player.Interact.phase == InputActionPhase.Waiting)
        {
            print("end shoip comntiolo task");
            spaceShipMovement.GoDown();
            EndTask();
        }
    }

    public override void StartTask(Interactable interactable)
    {
        base.StartTask(interactable);
    }

    public override void EndTask()
    {
        base.EndTask();
        spaceShipMovement.GoDown();
    }
}
