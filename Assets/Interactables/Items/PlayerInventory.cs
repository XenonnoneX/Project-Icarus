using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    ItemData currentItem;

    public delegate void OnItemChanged();
    public event OnItemChanged onItemChanged;
    
    public ItemData GetCurrentItem() => currentItem;
    public void SetCurrentItem(ItemData item) 
    { 
        currentItem = item;
        onItemChanged?.Invoke();
    }

    public void PickUpItem(ItemData item)
    {
        if (currentItem != null) OnDropItem();

        SetCurrentItem(item);
    }

    public void OnDropItem()
    {
        if (currentItem == null) return;

        GameObject itemDrop = (GameObject) Instantiate(currentItem.itemPrefab, transform.position, transform.rotation);
        itemDrop.GetComponent<PickupItem>().SetItemData(currentItem);

        SetCurrentItem(null);
    }
}
