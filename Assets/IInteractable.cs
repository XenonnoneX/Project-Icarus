using UnityEngine;

public interface IInteractable
{
    public Transform myTransform {  get; set; }
    public void ShowInteractable();
    public void HideInteractable();
    public void Interact();
    public void InteractEnd();
}
