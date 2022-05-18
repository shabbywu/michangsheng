using System;
using System.Collections.Generic;

// Token: 0x020001F6 RID: 502
[Serializable]
public class Blueprint
{
	// Token: 0x06001013 RID: 4115 RVA: 0x0001022C File Offset: 0x0000E42C
	public Blueprint(List<int> ingredients, int amountOfFinalItem, List<int> amount, Item item)
	{
		this.ingredients = ingredients;
		this.amount = amount;
		this.finalItem = item;
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00010260 File Offset: 0x0000E460
	public Blueprint()
	{
	}

	// Token: 0x04000C81 RID: 3201
	public List<int> ingredients = new List<int>();

	// Token: 0x04000C82 RID: 3202
	public List<int> amount = new List<int>();

	// Token: 0x04000C83 RID: 3203
	public Item finalItem;

	// Token: 0x04000C84 RID: 3204
	public int amountOfFinalItem;

	// Token: 0x04000C85 RID: 3205
	public float timeToCraft;
}
