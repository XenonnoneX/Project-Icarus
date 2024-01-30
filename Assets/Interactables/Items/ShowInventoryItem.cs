using UnityEngine;

public class ShowInventoryItem : MonoBehaviour
{
    PlayerInventory playerInventory;

    [SerializeField] SpriteRenderer itemSpriteRenderer;
    public SpriteRenderer GetItemSpriteRenderer() => itemSpriteRenderer;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.onItemChanged += ShowItem;
    }

    private void ShowItem()
    {
        if (playerInventory.GetCurrentItem() == null) itemSpriteRenderer.sprite = null;
        else itemSpriteRenderer.sprite = playerInventory.GetCurrentItem().sprite;


    }
}