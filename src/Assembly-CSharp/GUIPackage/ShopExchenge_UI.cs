using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage;

public class ShopExchenge_UI : Shop_UI
{
	public int ShopID = -1;

	public bool IsCangJinGe;

	public List<int> CangJinGeShop = new List<int>();

	private void Start()
	{
		InitExChengShopItems(0);
	}

	public void init(int shopID)
	{
		ShopID = shopID;
		InitExChengMethod(0);
	}

	public override int getExShopID(int type)
	{
		return ShopID;
	}

	public override int getShopType(int type)
	{
		return 0;
	}

	public override void updateItem()
	{
		int num = 0;
		foreach (Inventory2 inv in invList)
		{
			InitExShopPage(selectList[num].NowIndex, inv, num + 1);
			num++;
		}
	}

	public new void closeShop()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public override bool ShouldSetLevel(int Type = 0)
	{
		return false;
	}
}
