using System;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    PlayerInventory playerInventory;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject showInteractable;

    [SerializeField] ItemData itemData;

    public Transform myTransform { get; set; }
    public Action onInteractEnd { get; set; }

    bool outOfShip;

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

    private void Update()
    {
        if(!outOfShip) CheckOutOfShip();
    }

    public void CheckOutOfShip()
    {
        if (Utils.OutOfShip(transform))
        {
            SetOutOfShip();
        }
    }

    public void SetOutOfShip()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        GetComponent<Collider2D>().isTrigger = false;
        outOfShip = true;
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
        onInteractEnd?.Invoke();
    }

    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        spriteRenderer.sprite = itemData.sprite;
    }

    public void CancelTask()
    {
        
    }
}
