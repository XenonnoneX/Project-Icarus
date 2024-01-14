using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] PickupItem itemPrefab;
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

        PickupItem itemDrop = Instantiate(itemPrefab, Utils.GetWalkablePosNextTo(transform.position, 1), transform.rotation);
        itemDrop.SetItemData(currentItem);

        SetCurrentItem(null);
    }
}
