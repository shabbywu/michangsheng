using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class LianDanInventory : Inventory2
{
	// Token: 0x06001D07 RID: 7431 RVA: 0x00100190 File Offset: 0x000FE390
	private List<ITEM_INFO> getRealItemList()
	{
		Avatar player = Tools.instance.getPlayer();
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		for (int i = 0; i < player.itemList.values.Count; i++)
		{
			bool flag = true;
			for (int j = 25; j < 31; j++)
			{
				if (this.inventory[j].UUID == player.itemList.values[i].uuid && (long)this.inventory[j].itemNum >= (long)((ulong)player.itemList.values[i].itemCount))
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

	// Token: 0x06001D08 RID: 7432 RVA: 0x00100260 File Offset: 0x000FE460
	public new void Awake()
	{
		this.nowIndex = 0;
		this.datebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
		this.draggingItem = false;
		this.dragedItem = new item();
		if (!this.shouldInit)
		{
			return;
		}
		for (int i = 0; i < (int)this.count; i++)
		{
			this.inventory.Add(new item());
		}
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x0001836C File Offset: 0x0001656C
	public void selectPingJieAll()
	{
		this.Quaily = 0;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0001837B File Offset: 0x0001657B
	public void selectPingJie1()
	{
		this.Quaily = 1;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x0001838A File Offset: 0x0001658A
	public void selectPingJie2()
	{
		this.Quaily = 2;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x00018399 File Offset: 0x00016599
	public void selectPingJie3()
	{
		this.Quaily = 3;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x000183A8 File Offset: 0x000165A8
	public void selectPingJie4()
	{
		this.Quaily = 4;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x000183B7 File Offset: 0x000165B7
	public void selectPingJie5()
	{
		this.Quaily = 5;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x000183C6 File Offset: 0x000165C6
	public void selectPingJie6()
	{
		this.Quaily = 6;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x000042DD File Offset: 0x000024DD
	public new void Update()
	{
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x001002C8 File Offset: 0x000FE4C8
	public void LoadCaiLiaoInventory()
	{
		this.resteInventoryItem();
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		foreach (ITEM_INFO item_INFO in this.getRealItemList())
		{
			if (player.FindEquipItemByUUID(item_INFO.uuid) == null)
			{
				if (!jsonData.instance.ItemJsonData.ContainsKey(string.Concat(item_INFO.itemId)))
				{
					Debug.LogError("找不到物品" + item_INFO.itemId);
				}
				else if (((int)jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["quality"].n == this.Quaily || this.Quaily == 0) && (jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["type"].I == this.selectType || this.selectType == -1))
				{
					if (this.isInPage(this.nowIndex, num, 25))
					{
						this.addItemToNullInventory(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
					}
					num++;
				}
			}
		}
		if (this.lianDanSelectPage != null)
		{
			this.lianDanSelectPage.setMaxPage(player.itemList.values.Count / (int)this.FanYeCount + 1);
		}
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x00100470 File Offset: 0x000FE670
	public new void resteInventoryItem()
	{
		for (int i = 0; i < 25; i++)
		{
			this.inventory[i] = new item();
			this.inventory[i].itemNum = 1;
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x001004B0 File Offset: 0x000FE6B0
	public void resterLianDanFinshCell()
	{
		for (int i = 31; i < 37; i++)
		{
			this.inventory[i] = new item();
			this.inventory[i].itemNum = 1;
		}
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x001004F0 File Offset: 0x000FE6F0
	public new int addItemToNullInventory(int id, int num, string uuid, JSONObject Seid)
	{
		for (int i = 25; i < 37; i++)
		{
			if (this.inventory[i].UUID == uuid)
			{
				if (this.inventory[i].itemNum >= num)
				{
					return -1;
				}
				num -= this.inventory[i].itemNum;
			}
		}
		for (int j = 0; j < 25; j++)
		{
			if (this.inventory[j].itemID == -1)
			{
				this.inventory[j] = this.datebase.items[id].Clone();
				this.inventory[j].UUID = uuid;
				this.inventory[j].Seid = Seid;
				this.inventory[j].itemNum = num;
				return j;
			}
		}
		return 0;
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x000183D5 File Offset: 0x000165D5
	public new bool isInPage(int curpage, int itemIndex, int pagesize)
	{
		return itemIndex >= curpage * pagesize && itemIndex < (curpage + 1) * pagesize;
	}

	// Token: 0x0400190D RID: 6413
	public SelectLianDanPage lianDanSelectPage;

	// Token: 0x0400190E RID: 6414
	public int Quaily;

	// Token: 0x0400190F RID: 6415
	public int selectType = -1;
}
