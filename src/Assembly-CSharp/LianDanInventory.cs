using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class LianDanInventory : Inventory2
{
	// Token: 0x060019E5 RID: 6629 RVA: 0x000B98E8 File Offset: 0x000B7AE8
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

	// Token: 0x060019E6 RID: 6630 RVA: 0x000B99B8 File Offset: 0x000B7BB8
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

	// Token: 0x060019E7 RID: 6631 RVA: 0x000B9A1D File Offset: 0x000B7C1D
	public void selectPingJieAll()
	{
		this.Quaily = 0;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x000B9A2C File Offset: 0x000B7C2C
	public void selectPingJie1()
	{
		this.Quaily = 1;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x000B9A3B File Offset: 0x000B7C3B
	public void selectPingJie2()
	{
		this.Quaily = 2;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x000B9A4A File Offset: 0x000B7C4A
	public void selectPingJie3()
	{
		this.Quaily = 3;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x000B9A59 File Offset: 0x000B7C59
	public void selectPingJie4()
	{
		this.Quaily = 4;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x000B9A68 File Offset: 0x000B7C68
	public void selectPingJie5()
	{
		this.Quaily = 5;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x000B9A77 File Offset: 0x000B7C77
	public void selectPingJie6()
	{
		this.Quaily = 6;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x00004095 File Offset: 0x00002295
	public new void Update()
	{
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x000B9A88 File Offset: 0x000B7C88
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

	// Token: 0x060019F0 RID: 6640 RVA: 0x000B9C30 File Offset: 0x000B7E30
	public new void resteInventoryItem()
	{
		for (int i = 0; i < 25; i++)
		{
			this.inventory[i] = new item();
			this.inventory[i].itemNum = 1;
		}
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x000B9C70 File Offset: 0x000B7E70
	public void resterLianDanFinshCell()
	{
		for (int i = 31; i < 37; i++)
		{
			this.inventory[i] = new item();
			this.inventory[i].itemNum = 1;
		}
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x000B9CB0 File Offset: 0x000B7EB0
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

	// Token: 0x060019F3 RID: 6643 RVA: 0x000B9D8C File Offset: 0x000B7F8C
	public new bool isInPage(int curpage, int itemIndex, int pagesize)
	{
		return itemIndex >= curpage * pagesize && itemIndex < (curpage + 1) * pagesize;
	}

	// Token: 0x04001509 RID: 5385
	public SelectLianDanPage lianDanSelectPage;

	// Token: 0x0400150A RID: 5386
	public int Quaily;

	// Token: 0x0400150B RID: 5387
	public int selectType = -1;
}
