using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000A5F RID: 2655
	[Serializable]
	public class PaiMaiDataMag
	{
		// Token: 0x06004469 RID: 17513 RVA: 0x001D3C10 File Offset: 0x001D1E10
		public PaiMaiData GetShopInfo(int paiMaiId)
		{
			if (this.PaiMaiDict.Count == 0)
			{
				this.AuToUpDate();
			}
			else if (this.PaiMaiDict[paiMaiId].IsJoined)
			{
				this.UpdateById(paiMaiId);
			}
			if (!this.PaiMaiDict.ContainsKey(paiMaiId))
			{
				Debug.LogError(string.Format("没有拍卖会的商品数据，拍卖会Id:{0}", paiMaiId));
				return null;
			}
			return this.PaiMaiDict[paiMaiId];
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x001D3C80 File Offset: 0x001D1E80
		private void Init()
		{
			this.PaiMaiShopTypeDict = new Dictionary<int, List<int>>();
			this.PaiMaiShopQualityDict = new Dictionary<int, List<int>>();
			foreach (PaiMaiBiao paiMaiBiao in PaiMaiBiao.DataList)
			{
				try
				{
					if (paiMaiBiao.Type.Count > 0)
					{
						this.PaiMaiShopTypeDict.Add(paiMaiBiao.PaiMaiID, new List<int>());
						this.PaiMaiShopQualityDict.Add(paiMaiBiao.PaiMaiID, new List<int>());
						for (int i = 0; i < paiMaiBiao.Type.Count; i++)
						{
							for (int j = 0; j < paiMaiBiao.quanzhong1[i]; j++)
							{
								this.PaiMaiShopTypeDict[paiMaiBiao.PaiMaiID].Add(paiMaiBiao.Type[i]);
								this.PaiMaiShopQualityDict[paiMaiBiao.PaiMaiID].Add(paiMaiBiao.quality[i]);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message);
					Debug.LogError("初始化拍卖会失败");
					Debug.LogError(string.Format("拍卖会ID：{0}", paiMaiBiao.PaiMaiID));
					Debug.LogError(string.Format("商品类型数目：{0},", paiMaiBiao.Type.Count) + string.Format("品阶数目：{0},权重数目{1}", paiMaiBiao.quality.Count, paiMaiBiao.quanzhong1.Count));
				}
			}
			foreach (PaiMaiBiao paiMaiBiao2 in PaiMaiBiao.DataList)
			{
				if (this.PaiMaiDict.Count < PaiMaiBiao.DataList.Count && !this.PaiMaiDict.ContainsKey(paiMaiBiao2.PaiMaiID))
				{
					PaiMaiData paiMaiData = new PaiMaiData();
					paiMaiData.Id = paiMaiBiao2.PaiMaiID;
					paiMaiData.IsJoined = false;
					paiMaiData.No = this.GetNowPaiMaiJieNum(paiMaiBiao2.PaiMaiID);
					paiMaiData.ShopList = this.RandomPaiMaiShopList(paiMaiData.Id);
					if (paiMaiData.No > 0)
					{
						paiMaiData.NextUpdateTime = DateTime.Parse(PaiMaiBiao.DataDict[paiMaiBiao2.PaiMaiID].EndTime).AddYears((paiMaiData.No - 1) * paiMaiBiao2.circulation).AddDays(1.0);
					}
					else
					{
						paiMaiData.No = 1;
						paiMaiData.NextUpdateTime = DateTime.Parse(PaiMaiBiao.DataDict[paiMaiBiao2.PaiMaiID].EndTime).AddDays(1.0);
					}
					this.PaiMaiDict.Add(paiMaiData.Id, paiMaiData);
				}
			}
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x001D3FA8 File Offset: 0x001D21A8
		public bool IsJoin(int id)
		{
			if (this.PaiMaiDict.ContainsKey(id))
			{
				return this.PaiMaiDict[id].IsJoined;
			}
			this.Init();
			if (this.PaiMaiDict.ContainsKey(id))
			{
				return this.PaiMaiDict[id].IsJoined;
			}
			Debug.LogError(string.Format("不存在此拍卖会的Id{0}", id));
			return false;
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x001D4014 File Offset: 0x001D2214
		public void AuToUpDate()
		{
			if (!this.IsInit)
			{
				this.Init();
				this.IsInit = true;
			}
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			foreach (int num in this.PaiMaiDict.Keys)
			{
				if (nowTime >= this.PaiMaiDict[num].NextUpdateTime && PaiMaiBiao.DataDict[num].IsBuShuaXin == 0)
				{
					int num2 = (nowTime.Year - this.PaiMaiDict[num].NextUpdateTime.Year) / PaiMaiBiao.DataDict[num].circulation;
					if (num2 == 0)
					{
						this.UpdateById(num);
					}
					else
					{
						this.UpdateById(num, num2);
						if (nowTime > this.PaiMaiDict[num].NextUpdateTime)
						{
							this.UpdateById(num, 1);
						}
					}
				}
			}
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x001D4128 File Offset: 0x001D2328
		public void UpdateById(int id)
		{
			if (!this.IsInit)
			{
				this.Init();
				this.IsInit = true;
			}
			this.PaiMaiDict[id].No++;
			this.PaiMaiDict[id].NextUpdateTime = this.PaiMaiDict[id].NextUpdateTime.AddYears(PaiMaiBiao.DataDict[id].circulation);
			this.PaiMaiDict[id].ShopList = this.RandomPaiMaiShopList(id);
			this.PaiMaiDict[id].IsJoined = false;
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x001D41C4 File Offset: 0x001D23C4
		public void UpdateById(int id, int num)
		{
			if (!this.IsInit)
			{
				this.Init();
				this.IsInit = true;
			}
			this.PaiMaiDict[id].No += num;
			this.PaiMaiDict[id].NextUpdateTime = this.PaiMaiDict[id].NextUpdateTime.AddYears(PaiMaiBiao.DataDict[id].circulation * num);
			this.PaiMaiDict[id].ShopList = this.RandomPaiMaiShopList(id);
			this.PaiMaiDict[id].IsJoined = false;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x001D4264 File Offset: 0x001D2464
		public int GetNowPaiMaiJieNum(int id)
		{
			DateTime t = DateTime.Parse(PaiMaiBiao.DataDict[id].StarTime);
			DateTime dateTime = DateTime.Parse(PaiMaiBiao.DataDict[id].EndTime);
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			int num = 0;
			int circulation = PaiMaiBiao.DataDict[id].circulation;
			if (nowTime >= t)
			{
				num = nowTime.Year / circulation;
				if (nowTime > dateTime.AddYears((num - 1) * circulation))
				{
					num++;
				}
				return num;
			}
			return num;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x001D42F8 File Offset: 0x001D24F8
		private List<int> RandomPaiMaiShopList(int id)
		{
			List<int> list = new List<int>();
			int num = 0;
			if (PaiMaiBiao.DataDict[id].Type.Count == 0)
			{
				list.Add(PaiMaiBiao.DataDict[id].guding[0]);
			}
			else
			{
				int count = PaiMaiBiao.DataDict[id].guding.Count;
				int i = PaiMaiBiao.DataDict[id].ItemNum;
				int num2 = 0;
				if (count > 0)
				{
					num = PaiMaiBiao.DataDict[id].quanzhong2[0];
				}
				foreach (int num3 in PaiMaiBiao.DataDict[id].quanzhong1)
				{
					num2 += num3;
				}
				List<int> list2 = new List<int>();
				for (int j = 0; j < this.PaiMaiShopQualityDict[id].Count; j++)
				{
					list2.Add(j);
				}
				while (i > 0)
				{
					if (count > 0 && i == 1)
					{
						if (Tools.instance.GetRandomInt(0, num2) <= num)
						{
							list.Add(PaiMaiBiao.DataDict[id].guding[0]);
						}
						else
						{
							int i2;
							if (list2.Count > 0)
							{
								int randomInt = Tools.instance.GetRandomInt(0, list2.Count - 1);
								list2.RemoveAt(randomInt);
								i2 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(this.PaiMaiShopTypeDict[id][randomInt], this.PaiMaiShopQualityDict[id][randomInt])["id"].I;
							}
							else
							{
								do
								{
									int randomInt = Tools.instance.GetRandomInt(0, this.PaiMaiShopQualityDict[id].Count - 1);
									i2 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(this.PaiMaiShopTypeDict[id][randomInt], this.PaiMaiShopQualityDict[id][randomInt])["id"].I;
								}
								while (list.Contains(i2));
							}
							list.Add(i2);
						}
					}
					else
					{
						int i2;
						if (list2.Count > 0)
						{
							int randomInt = Tools.instance.GetRandomInt(0, list2.Count - 1);
							list2.RemoveAt(randomInt);
							i2 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(this.PaiMaiShopTypeDict[id][randomInt], this.PaiMaiShopQualityDict[id][randomInt])["id"].I;
						}
						else
						{
							do
							{
								int randomInt = Tools.instance.GetRandomInt(0, this.PaiMaiShopQualityDict[id].Count - 1);
								i2 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(this.PaiMaiShopTypeDict[id][randomInt], this.PaiMaiShopQualityDict[id][randomInt])["id"].I;
							}
							while (list.Contains(i2));
						}
						list.Add(i2);
					}
					i--;
				}
			}
			return list;
		}

		// Token: 0x04003C7D RID: 15485
		public Dictionary<int, PaiMaiData> PaiMaiDict = new Dictionary<int, PaiMaiData>();

		// Token: 0x04003C7E RID: 15486
		[NonSerialized]
		public bool IsInit;

		// Token: 0x04003C7F RID: 15487
		[NonSerialized]
		private Dictionary<int, List<int>> PaiMaiShopTypeDict;

		// Token: 0x04003C80 RID: 15488
		[NonSerialized]
		private Dictionary<int, List<int>> PaiMaiShopQualityDict;
	}
}
