using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    Item currentItem;
    public Item GetCurrentItem() => currentItem;
    public void SetCurrentItem(Item item) => currentItem = item;

    public void PickUpItem(Item item)
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
