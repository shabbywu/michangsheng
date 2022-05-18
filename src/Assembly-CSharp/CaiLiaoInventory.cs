using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class CaiLiaoInventory : Inventory2
{
	// Token: 0x06001E2A RID: 7722 RVA: 0x0001906D File Offset: 0x0001726D
	public void setWuWeiLeiXing()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = -1;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x00019095 File Offset: 0x00017295
	public void setWuWeiLeiXing1()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 1;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000190BD File Offset: 0x000172BD
	public void setWuWeiLeiXing2()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 2;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000190E5 File Offset: 0x000172E5
	public void setWuWeiLeiXing3()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 3;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x0001910D File Offset: 0x0001730D
	public void setWuWeiLeiXing4()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 4;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x00019135 File Offset: 0x00017335
	public void setWuWeiLeiXing5()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 5;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x00106B58 File Offset: 0x00104D58
	private List<ITEM_INFO> getRealItemList()
	{
		Avatar player = Tools.instance.getPlayer();
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		for (int i = 0; i < player.itemList.values.Count; i++)
		{
			bool flag = true;
			for (int j = 25; j < 35; j++)
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

	// Token: 0x06001E31 RID: 7729 RVA: 0x00106C28 File Offset: 0x00104E28
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
				else if (jsonData.instance.ItemJsonData[item_INFO.itemId.ToString()]["WuWeiType"].I != 0 && ((int)jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["quality"].n == this.Quaily || this.Quaily == 0) && (jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["WuWeiType"].I == this.WuWeiType || this.WuWeiType == -1))
				{
					if (this.isInPage(this.nowIndex, num, 25))
					{
						this.addItemToNullInventory(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
					}
					num++;
				}
			}
		}
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.setMaxPage(player.itemList.values.Count / (int)this.FanYeCount + 1);
		}
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x00100470 File Offset: 0x000FE670
	public new void resteInventoryItem()
	{
		for (int i = 0; i < 25; i++)
		{
			this.inventory[i] = new item();
			this.inventory[i].itemNum = 1;
		}
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x00106DFC File Offset: 0x00104FFC
	public new int addItemToNullInventory(int id, int num, string uuid, JSONObject Seid)
	{
		for (int i = 25; i < 35; i++)
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

	// Token: 0x06001E34 RID: 7732 RVA: 0x000183D5 File Offset: 0x000165D5
	public new bool isInPage(int curpage, int itemIndex, int pagesize)
	{
		return itemIndex >= curpage * pagesize && itemIndex < (curpage + 1) * pagesize;
	}

	// Token: 0x040019AA RID: 6570
	private int WuWeiType = -1;

	// Token: 0x040019AB RID: 6571
	public SelectCaiLiaoPage CaiLiaoselectpage;

	// Token: 0x040019AC RID: 6572
	public int Quaily;
}
