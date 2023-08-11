using GUIPackage;
using UnityEngine;

public class CaiLiaoItemEXCell : ItemCellEX
{
	public override void PCOnPress()
	{
		if (isCanClick())
		{
			putCaiLiao();
		}
	}

	private void putCaiLiao()
	{
	}

	private bool isCanClick()
	{
		if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
		{
			return Item.itemName != null;
		}
		return false;
	}

	private new void Update()
	{
		base.Update();
		if (Item.itemID != -1)
		{
			string str = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["name"].str;
			KeyObject.SetActive(true);
			KeyName.text = Tools.Code64(str);
		}
		else
		{
			KeyObject.SetActive(false);
		}
	}

	private void EXCaiLiao(ref item Item1, ref item Item2)
	{
		item item = Item1.Clone();
		item.itemNum = 1;
		int itemNum = Item1.itemNum;
		itemNum--;
		if (itemNum == 0)
		{
			Item1 = new item();
			Item2 = item;
		}
		else
		{
			Item1.itemNum = itemNum;
		}
	}

	public override void PCOnHover(bool isOver)
	{
		base.PCOnHover(isOver);
	}
}
