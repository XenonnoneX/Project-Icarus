using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BrokenInteractablePanel : MonoBehaviour
{
    PlayerInputs controls;

    [SerializeField] Slider slider;
    [SerializeField] float TimeToRepair = 3f;

    float repairTime;

    Interactable currentInteractable;

    private void Start()
    {
        controls = new PlayerInputs();
        controls.Enable();
        gameObject.SetActive(false);
    }

    void Update()
    {
        repairTime += controls.Player.Repair.ReadValue<float>() * Time.deltaTime;

        slider.value = repairTime / TimeToRepair;

        if (repairTime >= TimeToRepair)
        {
            currentInteractable.FixInteractable();
            gameObject.SetActive(false);
        }
    }
    public void SetCurrentInteractable(Interactable interactable)
    {
        repairTime = 0;
        currentInteractable = interactable;
    }
}