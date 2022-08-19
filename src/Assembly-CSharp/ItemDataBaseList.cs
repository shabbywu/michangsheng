using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class ItemDataBaseList : ScriptableObject
{
	// Token: 0x06000EAC RID: 3756 RVA: 0x0005990C File Offset: 0x00057B0C
	public Item getItemByID(int id)
	{
		for (int i = 0; i < this.itemList.Count; i++)
		{
			if (this.itemList[i].itemID == id)
			{
				return this.itemList[i].getCopy();
			}
		}
		return null;
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x00059958 File Offset: 0x00057B58
	public Item getItemByName(string name)
	{
		for (int i = 0; i < this.itemList.Count; i++)
		{
			if (this.itemList[i].itemName.ToLower().Equals(name.ToLower()))
			{
				return this.itemList[i].getCopy();
			}
		}
		return null;
	}

	// Token: 0x04000ABE RID: 2750
	[SerializeField]
	public List<Item> itemList = new List<Item>();
}
