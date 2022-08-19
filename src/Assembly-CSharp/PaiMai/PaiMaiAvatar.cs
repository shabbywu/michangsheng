using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai
{
	// Token: 0x02000718 RID: 1816
	[Serializable]
	public class PaiMaiAvatar
	{
		// Token: 0x06003A26 RID: 14886 RVA: 0x0018F3AC File Offset: 0x0018D5AC
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

		// Token: 0x06003A27 RID: 14887 RVA: 0x0018F670 File Offset: 0x0018D870
		public PaiMaiAvatar(string name)
		{
			this.Name = name;
			this.IsPlayer = true;
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x0018F688 File Offset: 0x0018D888
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

		// Token: 0x06003A29 RID: 14889 RVA: 0x0018FB48 File Offset: 0x0018DD48
		private bool IsLikeItem()
		{
			return jsonData.instance.GetMonstarInterestingItem(this.NpcId, SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId, null) > 0;
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x0018FB70 File Offset: 0x0018DD70
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

		// Token: 0x06003A2B RID: 14891 RVA: 0x0018FBDC File Offset: 0x0018DDDC
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

		// Token: 0x06003A2C RID: 14892 RVA: 0x0018FC48 File Offset: 0x0018DE48
		public void Select()
		{
			SingletonMono<PaiMaiUiMag>.Instance.SelectAvatarCallBack(this);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x0018FC58 File Offset: 0x0018DE58
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

		// Token: 0x04003238 RID: 12856
		public int NpcId;

		// Token: 0x04003239 RID: 12857
		public string Name;

		// Token: 0x0400323A RID: 12858
		public string Title;

		// Token: 0x0400323B RID: 12859
		public int Money;

		// Token: 0x0400323C RID: 12860
		public int Level;

		// Token: 0x0400323D RID: 12861
		public int ItemLevel;

		// Token: 0x0400323E RID: 12862
		public int MaxPrice;

		// Token: 0x0400323F RID: 12863
		public int ShenShi;

		// Token: 0x04003240 RID: 12864
		public PaiMaiAvatar.StateType State;

		// Token: 0x04003241 RID: 12865
		public List<int> NeedItemTagList;

		// Token: 0x04003242 RID: 12866
		public List<int> SuitableEquipIdList;

		// Token: 0x04003243 RID: 12867
		public bool IsPlayer;

		// Token: 0x04003244 RID: 12868
		public AvatarUI UiCtr;

		// Token: 0x04003245 RID: 12869
		public bool CanSelect;

		// Token: 0x02001534 RID: 5428
		public enum StateType
		{
			// Token: 0x04006EBF RID: 28351
			放弃 = 1,
			// Token: 0x04006EC0 RID: 28352
			略感兴趣,
			// Token: 0x04006EC1 RID: 28353
			跃跃欲试,
			// Token: 0x04006EC2 RID: 28354
			势在必得,
			// Token: 0x04006EC3 RID: 28355
			所有状态
		}
	}
}
