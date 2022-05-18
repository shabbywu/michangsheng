using System;
using System.Collections.Generic;
using JSONClass;

namespace PaiMai
{
	// Token: 0x02000A69 RID: 2665
	[Serializable]
	public class PaiMaiShop : IComparable<PaiMaiShop>
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x000311E0 File Offset: 0x0002F3E0
		// (set) Token: 0x060044B1 RID: 17585 RVA: 0x000311E8 File Offset: 0x0002F3E8
		public int Quality { get; private set; }

		// Token: 0x060044B2 RID: 17586 RVA: 0x001D6688 File Offset: 0x001D4888
		public void Init()
		{
			this.TagList = new List<int>();
			if (this.Seid.HasField("ItemFlag"))
			{
				this.TagList = this.Seid["ItemFlag"].ToList();
			}
			else
			{
				foreach (int item in _ItemJsonData.DataDict[this.ShopId].ItemFlag)
				{
					this.TagList.Add(item);
				}
			}
			if (this.Seid.HasField("Money"))
			{
				this.Price = this.Seid["Money"].I * this.Count;
			}
			else
			{
				this.Price *= this.Count;
			}
			if (this.Seid.HasField("Name"))
			{
				this.ShopName = this.Seid["Name"].Str;
			}
			else
			{
				this.ShopName = _ItemJsonData.DataDict[this.ShopId].name;
			}
			this.CurPrice = (int)((float)this.Price * 0.3f);
			this.Type = _ItemJsonData.DataDict[this.ShopId].type;
			if (this.Seid.HasField("quality"))
			{
				this.Quality = this.Seid["quality"].I;
			}
			else
			{
				this.Quality = _ItemJsonData.DataDict[this.ShopId].quality;
			}
			this.BaseQuality = this.Quality;
			if (this.Type == 1 || this.Type == 2)
			{
				this.Quality++;
			}
			else if (this.Type == 3 || this.Type == 4)
			{
				this.Quality *= 2;
			}
			this.MinAddPrice = (int)((double)this.Price * 0.02);
			if (this.MinAddPrice == 0)
			{
				this.MinAddPrice = 1;
			}
			this.Level = -1;
			if (this.TagList.Contains(50))
			{
				this.Level = 0;
			}
			else if (this.TagList.Contains(51))
			{
				this.Level = 1;
			}
			else if (this.TagList.Contains(52))
			{
				this.Level = 2;
			}
			if (this.Level >= 0)
			{
				this.MayPrice = this.Price + (int)((float)this.Price * 0.01f * (float)PaiMaiPanDing.DataDict[21 + this.Level].JiaWei);
				return;
			}
			this.MayPrice = this.Price;
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x000311F1 File Offset: 0x0002F3F1
		public int CompareTo(PaiMaiShop other)
		{
			return this.Price.CompareTo(other.Price);
		}

		// Token: 0x04003CB4 RID: 15540
		public int ShopId;

		// Token: 0x04003CB5 RID: 15541
		public int Count;

		// Token: 0x04003CB6 RID: 15542
		public int Price;

		// Token: 0x04003CB7 RID: 15543
		public int CurPrice;

		// Token: 0x04003CB8 RID: 15544
		public int MinAddPrice;

		// Token: 0x04003CB9 RID: 15545
		public int MayPrice;

		// Token: 0x04003CBA RID: 15546
		public bool IsPlayer;

		// Token: 0x04003CBB RID: 15547
		public string ShopName;

		// Token: 0x04003CBC RID: 15548
		public int Type;

		// Token: 0x04003CBE RID: 15550
		public int BaseQuality;

		// Token: 0x04003CBF RID: 15551
		public int Level;

		// Token: 0x04003CC0 RID: 15552
		public List<int> TagList;

		// Token: 0x04003CC1 RID: 15553
		public PaiMaiAvatar Owner;

		// Token: 0x04003CC2 RID: 15554
		public JSONObject Seid;
	}
}
