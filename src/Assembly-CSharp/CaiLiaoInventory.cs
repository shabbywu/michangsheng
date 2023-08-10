using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class CaiLiaoInventory : Inventory2
{
	private int WuWeiType = -1;

	public SelectCaiLiaoPage CaiLiaoselectpage;

	public int Quaily;

	public void setWuWeiLeiXing()
	{
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.RestePageIndex();
		}
		WuWeiType = -1;
		LoadCaiLiaoInventory();
	}

	public void setWuWeiLeiXing1()
	{
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.RestePageIndex();
		}
		WuWeiType = 1;
		LoadCaiLiaoInventory();
	}

	public void setWuWeiLeiXing2()
	{
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.RestePageIndex();
		}
		WuWeiType = 2;
		LoadCaiLiaoInventory();
	}

	public void setWuWeiLeiXing3()
	{
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.RestePageIndex();
		}
		WuWeiType = 3;
		LoadCaiLiaoInventory();
	}

	public void setWuWeiLeiXing4()
	{
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.RestePageIndex();
		}
		WuWeiType = 4;
		LoadCaiLiaoInventory();
	}

	public void setWuWeiLeiXing5()
	{
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.RestePageIndex();
		}
		WuWeiType = 5;
		LoadCaiLiaoInventory();
	}

	private List<ITEM_INFO> getRealItemList()
	{
		Avatar player = Tools.instance.getPlayer();
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		for (int i = 0; i < player.itemList.values.Count; i++)
		{
			bool flag = true;
			for (int j = 25; j < 35; j++)
			{
				if (inventory[j].UUID == player.itemList.values[i].uuid && inventory[j].itemNum >= player.itemList.values[i].itemCount)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list.Add(player.itemList.values[i]);
			}
		}
		return list;
	}

	public void LoadCaiLiaoInventory()
	{
		resteInventoryItem();
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (ITEM_INFO realItem in getRealItemList())
		{
			if (player.FindEquipItemByUUID(realItem.uuid) != null)
			{
				continue;
			}
			if (!jsonData.instance.ItemJsonData.ContainsKey(string.Concat(realItem.itemId)))
			{
				Debug.LogError((object)("找不到物品" + realItem.itemId));
			}
			else if (jsonData.instance.ItemJsonData[realItem.itemId.ToString()]["WuWeiType"].I != 0 && ((int)jsonData.instance.ItemJsonData[string.Concat(realItem.itemId)]["quality"].n == Quaily || Quaily == 0) && (jsonData.instance.ItemJsonData[string.Concat(realItem.itemId)]["WuWeiType"].I == WuWeiType || WuWeiType == -1))
			{
				if (isInPage(nowIndex, num, 25))
				{
					addItemToNullInventory(realItem.itemId, (int)realItem.itemCount, realItem.uuid, realItem.Seid);
				}
				num++;
			}
		}
		if ((Object)(object)CaiLiaoselectpage != (Object)null)
		{
			CaiLiaoselectpage.setMaxPage(player.itemList.values.Count / (int)FanYeCount + 1);
		}
	}

	public new void resteInventoryItem()
	{
		for (int i = 0; i < 25; i++)
		{
			inventory[i] = new item();
			inventory[i].itemNum = 1;
		}
	}

	public new int addItemToNullInventory(int id, int num, string uuid, JSONObject Seid)
	{
		for (int i = 25; i < 35; i++)
		{
			if (inventory[i].UUID == uuid)
			{
				if (inventory[i].itemNum >= num)
				{
					return -1;
				}
				num -= inventory[i].itemNum;
			}
		}
		for (int j = 0; j < 25; j++)
		{
			if (inventory[j].itemID == -1)
			{
				inventory[j] = datebase.items[id].Clone();
				inventory[j].UUID = uuid;
				inventory[j].Seid = Seid;
				inventory[j].itemNum = num;
				return j;
			}
		}
		return 0;
	}

	public new bool isInPage(int curpage, int itemIndex, int pagesize)
	{
		if (itemIndex >= curpage * pagesize && itemIndex < (curpage + 1) * pagesize)
		{
			return true;
		}
		return false;
	}
}
