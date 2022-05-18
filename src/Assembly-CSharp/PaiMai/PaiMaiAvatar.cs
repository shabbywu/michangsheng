using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000A6A RID: 2666
	[Serializable]
	public class PaiMaiAvatar
	{
		// Token: 0x060044B5 RID: 17589 RVA: 0x001D6944 File Offset: 0x001D4B44
		public PaiMaiAvatar(int id)
		{
			this.NpcId = id;
			this.Name = jsonData.instance.AvatarRandomJsonData[this.NpcId.ToString()]["Name"].Str;
			this.Title = jsonData.instance.AvatarJsonData[id.ToString()]["Title"].Str;
			this.ShenShi = jsonData.instance.AvatarJsonData[id.ToString()]["shengShi"].I;
			this.MaxPrice = 0;
			this.NeedItemTagList = new List<int>();
			this.SuitableEquipIdList = new List<int>();
			if (this.NpcId < 20000)
			{
				this.Money = PaiMaiOldAvatar.DataDict[this.NpcId].LingShi;
			}
			else
			{
				this.Money = jsonData.instance.AvatarBackpackJsonData[this.NpcId.ToString()]["money"].I;
				JSONObject jsonobject = this.NpcId.NPCJson();
				if (jsonobject.HasField("Status"))
				{
					int i = jsonobject["Status"]["StatusId"].I;
					if (i == 6)
					{
						this.NeedItemTagList.Add(620);
					}
					if (i == 2)
					{
						if (this.Level == 3)
						{
							this.NeedItemTagList.Add(610);
						}
						if (this.Level == 6)
						{
							this.NeedItemTagList.Add(611);
						}
						if (this.Level == 9)
						{
							this.NeedItemTagList.Add(612);
						}
						if (this.Level == 12)
						{
							this.NeedItemTagList.Add(3);
						}
						if (this.Level == 15)
						{
							this.NeedItemTagList.Add(614);
						}
					}
				}
				List<int> collection = jsonData.instance.AvatarJsonData[this.NpcId.ToString()]["equipWeaponPianHao"].ToList();
				List<int> collection2 = jsonData.instance.AvatarJsonData[this.NpcId.ToString()]["equipClothingPianHao"].ToList();
				List<int> collection3 = jsonData.instance.AvatarJsonData[this.NpcId.ToString()]["equipRingPianHao"].ToList();
				this.SuitableEquipIdList.AddRange(collection);
				this.SuitableEquipIdList.AddRange(collection2);
				this.SuitableEquipIdList.AddRange(collection3);
			}
			this.Level = jsonData.instance.AvatarJsonData[this.NpcId.ToString()]["Level"].I;
			this.ItemLevel = (this.Level - 1) / 3 + 1;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x00031204 File Offset: 0x0002F404
		public PaiMaiAvatar(string name)
		{
			this.Name = name;
			this.IsPlayer = true;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x001D6C08 File Offset: 0x001D4E08
		public void ThinKCurShop()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (SingletonMono<PaiMaiUiMag>.Instance.CurShop == null)
			{
				Debug.LogError("当前商品为空,放弃思考");
				this.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, false);
				return;
			}
			if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice > this.Money)
			{
				this.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, false);
				return;
			}
			if (this.IsNeedItem())
			{
				num += PaiMaiPanDing.DataDict[2].JiaWei;
				num2 += PaiMaiPanDing.DataDict[2].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[2].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[2].LueGanXingQu;
			}
			else if (this.IsLikeItem())
			{
				num += PaiMaiPanDing.DataDict[1].JiaWei;
				num2 += PaiMaiPanDing.DataDict[1].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[1].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[1].LueGanXingQu;
			}
			else if (this.IsSuitableEquip())
			{
				num += PaiMaiPanDing.DataDict[4].JiaWei;
				num2 += PaiMaiPanDing.DataDict[4].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[4].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[4].LueGanXingQu;
			}
			else
			{
				num += PaiMaiPanDing.DataDict[3].JiaWei;
				num2 += PaiMaiPanDing.DataDict[3].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[3].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[3].LueGanXingQu;
			}
			if (this.ItemLevel > SingletonMono<PaiMaiUiMag>.Instance.CurShop.Quality)
			{
				num += PaiMaiPanDing.DataDict[11].JiaWei;
				num2 += PaiMaiPanDing.DataDict[11].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[11].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[11].LueGanXingQu;
			}
			else if (this.ItemLevel == SingletonMono<PaiMaiUiMag>.Instance.CurShop.Quality)
			{
				num += PaiMaiPanDing.DataDict[12].JiaWei;
				num2 += PaiMaiPanDing.DataDict[12].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[12].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[12].LueGanXingQu;
			}
			else
			{
				num += PaiMaiPanDing.DataDict[13].JiaWei;
				num2 += PaiMaiPanDing.DataDict[13].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[13].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[13].LueGanXingQu;
			}
			if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level >= 0)
			{
				num += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].JiaWei;
				num2 += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].ShiZaiBiDe;
				num3 += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].YueYueYuShi;
				num4 += PaiMaiPanDing.DataDict[21 + SingletonMono<PaiMaiUiMag>.Instance.CurShop.Level].LueGanXingQu;
			}
			int randomInt = Tools.instance.GetRandomInt(0, 100);
			int randomInt2 = Tools.instance.GetRandomInt(0, 100);
			int randomInt3 = Tools.instance.GetRandomInt(0, 100);
			if (randomInt <= num2)
			{
				this.UiCtr.SetState(PaiMaiAvatar.StateType.势在必得, false);
			}
			else if (randomInt2 <= num3)
			{
				this.UiCtr.SetState(PaiMaiAvatar.StateType.跃跃欲试, false);
			}
			else if (randomInt3 <= num4)
			{
				this.UiCtr.SetState(PaiMaiAvatar.StateType.略感兴趣, false);
			}
			else
			{
				this.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, false);
			}
			if (this.State != PaiMaiAvatar.StateType.放弃)
			{
				num += PaiMaiPanDing.DataDict[(int)(100 + this.State)].JiaWei;
				this.MaxPrice = num * SingletonMono<PaiMaiUiMag>.Instance.CurShop.Price / 100;
				if (this.MaxPrice > this.Money)
				{
					this.MaxPrice = this.Money;
				}
				else if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice > this.MaxPrice)
				{
					this.UiCtr.SetState(PaiMaiAvatar.StateType.放弃, false);
				}
				Debug.Log(string.Format("npcId:{0},商品原价：{1},加成百分比{2},心理价位：{3}", new object[]
				{
					this.NpcId,
					SingletonMono<PaiMaiUiMag>.Instance.CurShop.Price,
					num,
					this.MaxPrice
				}));
			}
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x0003121A File Offset: 0x0002F41A
		private bool IsLikeItem()
		{
			return jsonData.instance.GetMonstarInterestingItem(this.NpcId, SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId, null) > 0;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x001D70C8 File Offset: 0x001D52C8
		private bool IsNeedItem()
		{
			foreach (int item in SingletonMono<PaiMaiUiMag>.Instance.CurShop.TagList)
			{
				if (this.NeedItemTagList.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x001D7134 File Offset: 0x001D5334
		private bool IsSuitableEquip()
		{
			foreach (int item in SingletonMono<PaiMaiUiMag>.Instance.CurShop.TagList)
			{
				if (this.SuitableEquipIdList.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x00031242 File Offset: 0x0002F442
		public void Select()
		{
			SingletonMono<PaiMaiUiMag>.Instance.SelectAvatarCallBack(this);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x001D71A0 File Offset: 0x001D53A0
		public void AddMaxMoney(float precent)
		{
			int num = (int)((float)this.MaxPrice * precent);
			if (num == 0)
			{
				if (precent >= 0f)
				{
					num = 1;
				}
				else
				{
					num = -1;
				}
			}
			this.MaxPrice += num;
			if (this.MaxPrice > this.Money)
			{
				this.MaxPrice = this.Money;
			}
			if (this.MaxPrice < 0)
			{
				this.MaxPrice = 0;
			}
		}

		// Token: 0x04003CC3 RID: 15555
		public int NpcId;

		// Token: 0x04003CC4 RID: 15556
		public string Name;

		// Token: 0x04003CC5 RID: 15557
		public string Title;

		// Token: 0x04003CC6 RID: 15558
		public int Money;

		// Token: 0x04003CC7 RID: 15559
		public int Level;

		// Token: 0x04003CC8 RID: 15560
		public int ItemLevel;

		// Token: 0x04003CC9 RID: 15561
		public int MaxPrice;

		// Token: 0x04003CCA RID: 15562
		public int ShenShi;

		// Token: 0x04003CCB RID: 15563
		public PaiMaiAvatar.StateType State;

		// Token: 0x04003CCC RID: 15564
		public List<int> NeedItemTagList;

		// Token: 0x04003CCD RID: 15565
		public List<int> SuitableEquipIdList;

		// Token: 0x04003CCE RID: 15566
		public bool IsPlayer;

		// Token: 0x04003CCF RID: 15567
		public AvatarUI UiCtr;

		// Token: 0x04003CD0 RID: 15568
		public bool CanSelect;

		// Token: 0x02000A6B RID: 2667
		public enum StateType
		{
			// Token: 0x04003CD2 RID: 15570
			放弃 = 1,
			// Token: 0x04003CD3 RID: 15571
			略感兴趣,
			// Token: 0x04003CD4 RID: 15572
			跃跃欲试,
			// Token: 0x04003CD5 RID: 15573
			势在必得,
			// Token: 0x04003CD6 RID: 15574
			所有状态
		}
	}
}
