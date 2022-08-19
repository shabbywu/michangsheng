using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class CaiLiaoInventory : Inventory2
{
	// Token: 0x06001B04 RID: 6916 RVA: 0x000C1131 File Offset: 0x000BF331
	public void setWuWeiLeiXing()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = -1;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x000C1159 File Offset: 0x000BF359
	public void setWuWeiLeiXing1()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 1;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x000C1181 File Offset: 0x000BF381
	public void setWuWeiLeiXing2()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 2;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x000C11A9 File Offset: 0x000BF3A9
	public void setWuWeiLeiXing3()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 3;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000C11D1 File Offset: 0x000BF3D1
	public void setWuWeiLeiXing4()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 4;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x000C11F9 File Offset: 0x000BF3F9
	public void setWuWeiLeiXing5()
	{
		if (this.CaiLiaoselectpage != null)
		{
			this.CaiLiaoselectpage.RestePageIndex();
		}
		this.WuWeiType = 5;
		this.LoadCaiLiaoInventory();
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x000C1224 File Offset: 0x000BF424
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

	// Token: 0x06001B0B RID: 6923 RVA: 0x000C12F4 File Offset: 0x000BF4F4
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

	// Token: 0x06001B0C RID: 6924 RVA: 0x000C14C8 File Offset: 0x000BF6C8
	public new void resteInventoryItem()
	{
		for (int i = 0; i < 25; i++)
		{
			this.inventory[i] = new item();
			this.inventory[i].itemNum = 1;
		}
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x000C1508 File Offset: 0x000BF708
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

	// Token: 0x06001B0E RID: 6926 RVA: 0x000B9D8C File Offset: 0x000B7F8C
	public new bool isInPage(int curpage, int itemIndex, int pagesize)
	{
		return itemIndex >= curpage * pagesize && itemIndex < (curpage + 1) * pagesize;
	}

	// Token: 0x0400159D RID: 5533
	private int WuWeiType = -1;

	// Token: 0x0400159E RID: 5534
	public SelectCaiLiaoPage CaiLiaoselectpage;

	// Token: 0x0400159F RID: 5535
	public int Quaily;
}
