using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class ItemDataBaseList : ScriptableObject
{
	// Token: 0x060010CC RID: 4300 RVA: 0x000A9CB4 File Offset: 0x000A7EB4
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

	// Token: 0x060010CD RID: 4301 RVA: 0x000A9D00 File Offset: 0x000A7F00
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

	// Token: 0x04000D59 RID: 3417
	[SerializeField]
	public List<Item> itemList = new List<Item>();
}
