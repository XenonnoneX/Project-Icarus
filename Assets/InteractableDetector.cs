using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    
    private IInteractable _interactable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _interactable = other.GetComponent<IInteractable>();

        if (_interactable != null)
        {
            _interactable.ShowInteractable();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_interactable != null)
        {
            _interactable.HideInteractable();
            _interactable = null;
        }
    }

    void OnInteract()
    {
        if (_interactable != null)
        {
            _interactable.Interact();
            playerMovement.enabled = false;
        }
    }

    public void OnInteractEnd()
    {
        if (_interactable != null)
        {
            _interactable.InteractEnd();
            playerMovement.enabled = true;
        }
    }
}