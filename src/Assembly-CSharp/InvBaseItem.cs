using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004C RID: 76
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000296 RID: 662
	public int id16;

	// Token: 0x04000297 RID: 663
	public string name;

	// Token: 0x04000298 RID: 664
	public string description;

	// Token: 0x04000299 RID: 665
	public InvBaseItem.Slot slot;

	// Token: 0x0400029A RID: 666
	public int minItemLevel = 1;

	// Token: 0x0400029B RID: 667
	public int maxItemLevel = 50;

	// Token: 0x0400029C RID: 668
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x0400029D RID: 669
	public GameObject attachment;

	// Token: 0x0400029E RID: 670
	public Color color = Color.white;

	// Token: 0x0400029F RID: 671
	public UIAtlas iconAtlas;

	// Token: 0x040002A0 RID: 672
	public string iconName = "";

	// Token: 0x0200004D RID: 77
	public enum Slot
	{
		// Token: 0x040002A2 RID: 674
		None,
		// Token: 0x040002A3 RID: 675
		Weapon,
		// Token: 0x040002A4 RID: 676
		Shield,
		// Token: 0x040002A5 RID: 677
		Body,
		// Token: 0x040002A6 RID: 678
		Shoulders,
		// Token: 0x040002A7 RID: 679
		Bracers,
		// Token: 0x040002A8 RID: 680
		Boots,
		// Token: 0x040002A9 RID: 681
		Trinket,
		// Token: 0x040002AA RID: 682
		_LastDoNotUse
	}
}
