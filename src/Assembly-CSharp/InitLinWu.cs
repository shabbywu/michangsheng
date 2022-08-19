using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

// Token: 0x02000437 RID: 1079
public class InitLinWu : MonoBehaviour
{
	// Token: 0x06002258 RID: 8792 RVA: 0x000EC981 File Offset: 0x000EAB81
	private void Start()
	{
		this.updateItem();
	}

	// Token: 0x06002259 RID: 8793 RVA: 0x000EC98C File Offset: 0x000EAB8C
	public void updateItem()
	{
		for (int i = 0; i < (int)this.inventory.count; i++)
		{
			this.inventory.inventory.Add(new item());
		}
		this.inventory = base.GetComponent<Inventory2>();
		this.setItem1();
		if (this.inventory.inventory[0].itemID != -1)
		{
			this.keyCell.keyItem = this.inventory.inventory[0];
		}
	}

	// Token: 0x0600225A RID: 8794 RVA: 0x000ECA0B File Offset: 0x000EAC0B
	public void setItem1()
	{
		this.inventory.setItemLeiXin(new List<int>
		{
			3,
			4,
			13
		});
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x000ECA33 File Offset: 0x000EAC33
	public void setItem2()
	{
		this.inventory.setItemLeiXin(new List<int>
		{
			3
		});
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x000ECA4C File Offset: 0x000EAC4C
	public void setItem3()
	{
		this.inventory.setItemLeiXin(new List<int>
		{
			4
		});
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x000ECA68 File Offset: 0x000EAC68
	public int getInputID(string name)
	{
		int num = 0;
		foreach (string b in this.mList.items)
		{
			if (name == b)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x000ECACC File Offset: 0x000EACCC
	public void chengeList()
	{
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		dictionary[0] = new List<int>
		{
			3,
			4
		};
		dictionary[1] = new List<int>
		{
			3
		};
		dictionary[2] = new List<int>
		{
			4
		};
		this.inventory.setItemLeiXin(dictionary[this.getInputID(this.mList.value)]);
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001BCD RID: 7117
	public Inventory2 inventory;

	// Token: 0x04001BCE RID: 7118
	public KeyCell keyCell;

	// Token: 0x04001BCF RID: 7119
	public UIPopupList mList;
}
