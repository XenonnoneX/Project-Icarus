public class Telescope : ControlStation
{
    ItemData currentLens;

    public delegate void OnLensChanged(ItemData lens);
    public event OnLensChanged onLensChanged;

    public override void CompleteTask(ItemData currentItem)
    {
        currentLens = currentItem;

        onLensChanged?.Invoke(currentLens);

        if (currentItem == null) print("Removed Lens");
        else print("Inserted Lens: " + currentItem.name);

        base.CompleteTask();
    }
}