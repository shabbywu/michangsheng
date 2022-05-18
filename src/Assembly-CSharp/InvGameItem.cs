using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000050 RID: 80
[Serializable]
public class InvGameItem
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x0600047F RID: 1151 RVA: 0x00007F6B File Offset: 0x0000616B
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000480 RID: 1152 RVA: 0x00007F73 File Offset: 0x00006173
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000481 RID: 1153 RVA: 0x00007F94 File Offset: 0x00006194
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000482 RID: 1154 RVA: 0x0006EA34 File Offset: 0x0006CC34
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000483 RID: 1155 RVA: 0x0006EB04 File Offset: 0x0006CD04
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result..ctor(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result..ctor(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result..ctor(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result..ctor(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return result;
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00007FC6 File Offset: 0x000061C6
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00007FE3 File Offset: 0x000061E3
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0006EC24 File Offset: 0x0006CE24
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x040002B2 RID: 690
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x040002B3 RID: 691
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x040002B4 RID: 692
	public int itemLevel = 1;

	// Token: 0x040002B5 RID: 693
	private InvBaseItem mBaseItem;

	// Token: 0x02000051 RID: 81
	public enum Quality
	{
		// Token: 0x040002B7 RID: 695
		Broken,
		// Token: 0x040002B8 RID: 696
		Cursed,
		// Token: 0x040002B9 RID: 697
		Damaged,
		// Token: 0x040002BA RID: 698
		Worn,
		// Token: 0x040002BB RID: 699
		Sturdy,
		// Token: 0x040002BC RID: 700
		Polished,
		// Token: 0x040002BD RID: 701
		Improved,
		// Token: 0x040002BE RID: 702
		Crafted,
		// Token: 0x040002BF RID: 703
		Superior,
		// Token: 0x040002C0 RID: 704
		Enchanted,
		// Token: 0x040002C1 RID: 705
		Epic,
		// Token: 0x040002C2 RID: 706
		Legendary,
		// Token: 0x040002C3 RID: 707
		_LastDoNotUse
	}
}
