using System;
using System.Collections.Generic;

// Token: 0x02000125 RID: 293
[Serializable]
public class Blueprint
{
	// Token: 0x06000E05 RID: 3589 RVA: 0x00052C32 File Offset: 0x00050E32
	public Blueprint(List<int> ingredients, int amountOfFinalItem, List<int> amount, Item item)
	{
		this.ingredients = ingredients;
		this.amount = amount;
		this.finalItem = item;
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x00052C66 File Offset: 0x00050E66
	public Blueprint()
	{
	}

	// Token: 0x040009E9 RID: 2537
	public List<int> ingredients = new List<int>();

	// Token: 0x040009EA RID: 2538
	public List<int> amount = new List<int>();

	// Token: 0x040009EB RID: 2539
	public Item finalItem;

	// Token: 0x040009EC RID: 2540
	public int amountOfFinalItem;

	// Token: 0x040009ED RID: 2541
	public float timeToCraft;
}
