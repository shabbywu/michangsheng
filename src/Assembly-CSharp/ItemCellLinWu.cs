using GUIPackage;

public class ItemCellLinWu : ItemCell
{
	public KeyCell keyCell;

	private void Start()
	{
	}

	private void OnPress()
	{
		if (Item.itemID != -1)
		{
			keyCell.keyItem = Item;
		}
	}

	public override int getItemPrice()
	{
		return 0;
	}
}
