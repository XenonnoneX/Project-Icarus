using System;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    PlayerInventory playerInventory;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject showInteractable;

    [SerializeField] Item itemData;

    public Transform myTransform { get; set; }
    public Action onInteractEnd { get; set; }

    private void Awake()
    {
        playerInventory = GameObject.FindObjectOfType<PlayerInventory>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        myTransform = transform;
    }

    private void Start()
    {
        HideInteractable();

        if (itemData != null)
        {
            SetItemData(itemData);
        }
    }

    public void ShowInteractable()
    {
        showInteractable.SetActive(true);
    }

    public void HideInteractable()
    {
        showInteractable.SetActive(false);
    }

    public void Interact()
    {
        playerInventory.PickUpItem(itemData);
        InteractEnd();
        Destroy(gameObject);
    }

    public void TakeItem()
    {
        playerInventory.PickUpItem(itemData);
        Destroy(gameObject);
    }

    public void InteractEnd()
    {
        onInteractEnd.Invoke();
    }

    public void SetItemData(Item itemData)
    {
        this.itemData = itemData;
        spriteRenderer.sprite = itemData.sprite;
    }
}
