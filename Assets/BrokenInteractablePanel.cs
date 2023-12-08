using UnityEngine;
using UnityEngine.UI;

public class BrokenInteractablePanel : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float TimeToRepair = 3f;

    float repairTime;

    Interactable currentInteractable;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        repairTime += Time.deltaTime;

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