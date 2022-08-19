using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003C RID: 60
[Serializable]
public class InvGameItem
{
	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000437 RID: 1079 RVA: 0x000174DD File Offset: 0x000156DD
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000438 RID: 1080 RVA: 0x000174E5 File Offset: 0x000156E5
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

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x00017506 File Offset: 0x00015706
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

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x00017538 File Offset: 0x00015738
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

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x0600043B RID: 1083 RVA: 0x00017608 File Offset: 0x00015808
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

	// Token: 0x0600043C RID: 1084 RVA: 0x00017728 File Offset: 0x00015928
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00017745 File Offset: 0x00015945
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001776C File Offset: 0x0001596C
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

	// Token: 0x04000262 RID: 610
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x04000263 RID: 611
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x04000264 RID: 612
	public int itemLevel = 1;

	// Token: 0x04000265 RID: 613
	private InvBaseItem mBaseItem;

	// Token: 0x020011D8 RID: 4568
	public enum Quality
	{
		// Token: 0x040063A0 RID: 25504
		Broken,
		// Token: 0x040063A1 RID: 25505
		Cursed,
		// Token: 0x040063A2 RID: 25506
		Damaged,
		// Token: 0x040063A3 RID: 25507
		Worn,
		// Token: 0x040063A4 RID: 25508
		Sturdy,
		// Token: 0x040063A5 RID: 25509
		Polished,
		// Token: 0x040063A6 RID: 25510
		Improved,
		// Token: 0x040063A7 RID: 25511
		Crafted,
		// Token: 0x040063A8 RID: 25512
		Superior,
		// Token: 0x040063A9 RID: 25513
		Enchanted,
		// Token: 0x040063AA RID: 25514
		Epic,
		// Token: 0x040063AB RID: 25515
		Legendary,
		// Token: 0x040063AC RID: 25516
		_LastDoNotUse
	}
}
