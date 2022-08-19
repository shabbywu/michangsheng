using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000039 RID: 57
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000250 RID: 592
	public int id16;

	// Token: 0x04000251 RID: 593
	public string name;

	// Token: 0x04000252 RID: 594
	public string description;

	// Token: 0x04000253 RID: 595
	public InvBaseItem.Slot slot;

	// Token: 0x04000254 RID: 596
	public int minItemLevel = 1;

	// Token: 0x04000255 RID: 597
	public int maxItemLevel = 50;

	// Token: 0x04000256 RID: 598
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x04000257 RID: 599
	public GameObject attachment;

	// Token: 0x04000258 RID: 600
	public Color color = Color.white;

	// Token: 0x04000259 RID: 601
	public UIAtlas iconAtlas;

	// Token: 0x0400025A RID: 602
	public string iconName = "";

	// Token: 0x020011D7 RID: 4567
	public enum Slot
	{
		// Token: 0x04006396 RID: 25494
		None,
		// Token: 0x04006397 RID: 25495
		Weapon,
		// Token: 0x04006398 RID: 25496
		Shield,
		// Token: 0x04006399 RID: 25497
		Body,
		// Token: 0x0400639A RID: 25498
		Shoulders,
		// Token: 0x0400639B RID: 25499
		Bracers,
		// Token: 0x0400639C RID: 25500
		Boots,
		// Token: 0x0400639D RID: 25501
		Trinket,
		// Token: 0x0400639E RID: 25502
		_LastDoNotUse
	}
}
