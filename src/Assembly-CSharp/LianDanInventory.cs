using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class LianDanInventory : Inventory2
{
	public SelectLianDanPage lianDanSelectPage;

	public int Quaily;

	public int selectType = -1;

	private List<ITEM_INFO> getRealItemList()
	{
		Avatar player = Tools.instance.getPlayer();
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		for (int i = 0; i < player.itemList.values.Count; i++)
		{
			bool flag = true;
			for (int j = 25; j < 31; j++)
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

	public new void Awake()
	{
		nowIndex = 0;
		datebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		draggingItem = false;
		dragedItem = new item();
		if (shouldInit)
		{
			for (int i = 0; i < (int)count; i++)
			{
				inventory.Add(new item());
			}
		}
	}

	public void selectPingJieAll()
	{
		Quaily = 0;
		LoadCaiLiaoInventory();
	}

	public void selectPingJie1()
	{
		Quaily = 1;
		LoadCaiLiaoInventory();
	}

	public void selectPingJie2()
	{
		Quaily = 2;
		LoadCaiLiaoInventory();
	}

	public void selectPingJie3()
	{
		Quaily = 3;
		LoadCaiLiaoInventory();
	}

	public void selectPingJie4()
	{
		Quaily = 4;
		LoadCaiLiaoInventory();
	}

	public void selectPingJie5()
	{
		Quaily = 5;
		LoadCaiLiaoInventory();
	}

	public void selectPingJie6()
	{
		Quaily = 6;
		LoadCaiLiaoInventory();
	}

	public new void Update()
	{
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
			else if (((int)jsonData.instance.ItemJsonData[string.Concat(realItem.itemId)]["quality"].n == Quaily || Quaily == 0) && (jsonData.instance.ItemJsonData[string.Concat(realItem.itemId)]["type"].I == selectType || selectType == -1))
			{
				if (isInPage(nowIndex, num, 25))
				{
					addItemToNullInventory(realItem.itemId, (int)realItem.itemCount, realItem.uuid, realItem.Seid);
				}
				num++;
			}
		}
		if ((Object)(object)lianDanSelectPage != (Object)null)
		{
			lianDanSelectPage.setMaxPage(player.itemList.values.Count / (int)FanYeCount + 1);
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

	public void resterLianDanFinshCell()
	{
		for (int i = 31; i < 37; i++)
		{
			inventory[i] = new item();
			inventory[i].itemNum = 1;
		}
	}

	public new int addItemToNullInventory(int id, int num, string uuid, JSONObject Seid)
	{
		for (int i = 25; i < 37; i++)
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
