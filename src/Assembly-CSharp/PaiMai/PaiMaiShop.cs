using System;
using System.Collections.Generic;
using JSONClass;

namespace PaiMai
{
	// Token: 0x02000717 RID: 1815
	[Serializable]
	public class PaiMaiShop : IComparable<PaiMaiShop>
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x0018F0CB File Offset: 0x0018D2CB
		// (set) Token: 0x06003A22 RID: 14882 RVA: 0x0018F0D3 File Offset: 0x0018D2D3
		public int Quality { get; private set; }

		// Token: 0x06003A23 RID: 14883 RVA: 0x0018F0DC File Offset: 0x0018D2DC
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

		// Token: 0x06003A24 RID: 14884 RVA: 0x0018F398 File Offset: 0x0018D598
		public int CompareTo(PaiMaiShop other)
		{
			return this.Price.CompareTo(other.Price);
		}

		// Token: 0x04003229 RID: 12841
		public int ShopId;

		// Token: 0x0400322A RID: 12842
		public int Count;

		// Token: 0x0400322B RID: 12843
		public int Price;

		// Token: 0x0400322C RID: 12844
		public int CurPrice;

		// Token: 0x0400322D RID: 12845
		public int MinAddPrice;

		// Token: 0x0400322E RID: 12846
		public int MayPrice;

		// Token: 0x0400322F RID: 12847
		public bool IsPlayer;

		// Token: 0x04003230 RID: 12848
		public string ShopName;

		// Token: 0x04003231 RID: 12849
		public int Type;

		// Token: 0x04003233 RID: 12851
		public int BaseQuality;

		// Token: 0x04003234 RID: 12852
		public int Level;

		// Token: 0x04003235 RID: 12853
		public List<int> TagList;

		// Token: 0x04003236 RID: 12854
		public PaiMaiAvatar Owner;

		// Token: 0x04003237 RID: 12855
		public JSONObject Seid;
	}
}
