using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    PlayerInputs controls;

    StringOfTimeAbility stringOfTimeAbility;
    [SerializeField] Dash dash;
    TunnelDash tunnelDash;
    InteractableDetector interactableDetector;

    private void Awake()
    {
        stringOfTimeAbility = FindObjectOfType<StringOfTimeAbility>();
        if(dash == null) dash = FindObjectOfType<Dash>();
        tunnelDash = FindObjectOfType<TunnelDash>();
        interactableDetector = FindObjectOfType<InteractableDetector>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerInputs();
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.StringOfTime.triggered) StringOfTime();
        if (controls.Player.Dash.triggered) Dash();
        if (controls.Player.Interact.triggered) Interact();
    }

    void StringOfTime()
    {
        stringOfTimeAbility.ActivateAbilityInput();
    }

    void Dash()
    {
        dash.ActivateAbilityInput();
        tunnelDash.ActivateAbilityInput();
    }

    void Interact()
    {
        interactableDetector.InteractInput();
    }
}
