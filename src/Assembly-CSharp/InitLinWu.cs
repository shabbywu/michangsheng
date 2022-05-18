using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

// Token: 0x020005EE RID: 1518
public class InitLinWu : MonoBehaviour
{
	// Token: 0x06002617 RID: 9751 RVA: 0x0001E697 File Offset: 0x0001C897
	private void Start()
	{
		this.updateItem();
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x0012D9FC File Offset: 0x0012BBFC
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

	// Token: 0x06002619 RID: 9753 RVA: 0x0001E69F File Offset: 0x0001C89F
	public void setItem1()
	{
		this.inventory.setItemLeiXin(new List<int>
		{
			3,
			4,
			13
		});
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x0001E6C7 File Offset: 0x0001C8C7
	public void setItem2()
	{
		this.inventory.setItemLeiXin(new List<int>
		{
			3
		});
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
	public void setItem3()
	{
		this.inventory.setItemLeiXin(new List<int>
		{
			4
		});
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x0012DA7C File Offset: 0x0012BC7C
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

	// Token: 0x0600261D RID: 9757 RVA: 0x0012DAE0 File Offset: 0x0012BCE0
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

	// Token: 0x0600261E RID: 9758 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002099 RID: 8345
	public Inventory2 inventory;

	// Token: 0x0400209A RID: 8346
	public KeyCell keyCell;

	// Token: 0x0400209B RID: 8347
	public UIPopupList mList;
}
