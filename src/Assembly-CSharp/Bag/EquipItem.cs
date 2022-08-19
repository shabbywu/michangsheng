using System;
using System.Collections.Generic;
using JSONClass;
using Tab;
using UnityEngine;

namespace Bag
{
	// Token: 0x020009A1 RID: 2465
	[Serializable]
	public class EquipItem : BaseItem
	{
		// Token: 0x060044CA RID: 17610 RVA: 0x001D41B4 File Offset: 0x001D23B4
		public override void SetItem(int id, int count, JSONObject seid)
		{
			base.SetItem(id, count, seid);
			if (seid.HasField("Name"))
			{
				this.Name = seid["Name"].Str;
			}
			if (seid.HasField("quality"))
			{
				this.Quality = seid["quality"].I;
			}
			if (seid.HasField("QPingZhi"))
			{
				this.PinJie = seid["QPingZhi"].I;
			}
			this.EquipType = this.GetEquipType();
			switch (this.EquipType)
			{
			case EquipType.武器:
				this.CanPutSlotType = CanSlotType.武器;
				break;
			case EquipType.防具:
				this.CanPutSlotType = CanSlotType.衣服;
				break;
			case EquipType.饰品:
				this.CanPutSlotType = CanSlotType.饰品;
				break;
			case EquipType.灵舟:
				this.CanPutSlotType = CanSlotType.灵舟;
				break;
			case EquipType.丹炉:
				this.CanPutSlotType = CanSlotType.丹炉;
				break;
			}
			this.EquipQuality = this.GetEquipQualityType();
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x001D429B File Offset: 0x001D249B
		public override string GetDesc1()
		{
			if (this.Seid.HasField("SeidDesc"))
			{
				return this.Seid["SeidDesc"].str;
			}
			return base.GetDesc1();
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x001D42CB File Offset: 0x001D24CB
		public override string GetDesc2()
		{
			if (this.Seid.HasField("Desc"))
			{
				return this.Seid["Desc"].str;
			}
			return base.GetDesc2();
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x001D42FB File Offset: 0x001D24FB
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.EquipType = this.GetEquipType();
			this.EquipQuality = this.GetEquipQualityType();
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001D4320 File Offset: 0x001D2520
		public override int GetImgQuality()
		{
			int result = this.Quality;
			if (this.EquipType != EquipType.丹炉 && this.EquipType != EquipType.灵舟)
			{
				result = this.Quality + 1;
			}
			return result;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x001D4350 File Offset: 0x001D2550
		public override List<int> GetCiZhui()
		{
			if (this.Seid.HasField("SkillSeids") || this.Seid.HasField("ItemSeids"))
			{
				List<int> list = new List<int>();
				if (this.EquipType == EquipType.武器)
				{
					List<JSONObject> list2 = this.Seid["SkillSeids"].list;
					for (int i = 0; i < list2.Count; i++)
					{
						int i2 = list2[i]["id"].I;
						if (i2 == 79 || i2 == 80)
						{
							for (int j = 0; j < list2[i]["value1"].Count; j++)
							{
								int i3 = list2[i]["value1"][j].I;
								for (int k = 0; k < _BuffJsonData.DataDict[i3].Affix.Count; k++)
								{
									int item = _BuffJsonData.DataDict[i3].Affix[k];
									if (!list.Contains(item))
									{
										list.Add(item);
									}
								}
							}
						}
					}
				}
				else
				{
					List<JSONObject> list3 = this.Seid["ItemSeids"].list;
					for (int l = 0; l < list3.Count; l++)
					{
						int i4 = list3[l]["id"].I;
						if (i4 == 5 || i4 == 17)
						{
							for (int m = 0; m < list3[l]["value1"].Count; m++)
							{
								int i3 = list3[l]["value1"][m].I;
								for (int n = 0; n < _BuffJsonData.DataDict[i3].Affix.Count; n++)
								{
									int item = _BuffJsonData.DataDict[i3].Affix[n];
									if (!list.Contains(item))
									{
										list.Add(item);
									}
								}
							}
						}
					}
				}
				return list;
			}
			return base.GetCiZhui();
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x001D4588 File Offset: 0x001D2788
		public override Sprite GetIconSprite()
		{
			if (this.Seid != null && this.Seid.HasField("ItemIcon"))
			{
				return ResManager.inst.LoadSprite(this.Seid["ItemIcon"].str);
			}
			return base.GetIconSprite();
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x001D45D8 File Offset: 0x001D27D8
		public override Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x001D4604 File Offset: 0x001D2804
		public override Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x001D4630 File Offset: 0x001D2830
		public override int GetPrice()
		{
			if (this.Seid != null && this.Seid.HasField("Money"))
			{
				return this.Seid["Money"].I;
			}
			if (this.Seid != null && this.Seid.HasField("NaiJiu"))
			{
				return (int)((float)base.GetPrice() * base.getItemNaiJiuPrice());
			}
			return base.GetPrice();
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x001D3742 File Offset: 0x001D1942
		public override int GetPlayerPrice()
		{
			return (int)((float)this.GetPrice() * 0.5f);
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x001D469D File Offset: 0x001D289D
		public override string GetName()
		{
			if (this.Seid != null && this.Seid.HasField("Name"))
			{
				return this.Seid["Name"].str;
			}
			return base.GetName();
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x001D46D8 File Offset: 0x001D28D8
		public EquipType GetEquipType()
		{
			EquipType result = EquipType.武器;
			int type = this.Type;
			switch (type)
			{
			case 0:
				result = EquipType.武器;
				break;
			case 1:
				result = EquipType.防具;
				break;
			case 2:
				result = EquipType.饰品;
				break;
			default:
				if (type != 9)
				{
					if (type == 14)
					{
						result = EquipType.灵舟;
					}
				}
				else
				{
					result = EquipType.丹炉;
				}
				break;
			}
			return result;
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x001D4720 File Offset: 0x001D2920
		public EquipQuality GetEquipQualityType()
		{
			EquipQuality result = EquipQuality.下品;
			switch (this.PinJie)
			{
			case 1:
				result = EquipQuality.下品;
				break;
			case 2:
				result = EquipQuality.中品;
				break;
			case 3:
				result = EquipQuality.上品;
				break;
			}
			return result;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x001D4757 File Offset: 0x001D2957
		public bool EquipTypeIsEqual(EquipType targetType)
		{
			if (targetType == EquipType.装备)
			{
				return this.EquipType == EquipType.武器 || this.EquipType == EquipType.防具 || this.EquipType == EquipType.饰品;
			}
			return targetType == this.EquipType;
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x001D4788 File Offset: 0x001D2988
		public override string GetQualityName()
		{
			if (this.EquipType == EquipType.丹炉 || this.EquipType == EquipType.灵舟)
			{
				return base.GetQualityName();
			}
			if (this.Seid.HasField("qualitydesc"))
			{
				return this.Seid["qualitydesc"].Str;
			}
			return this.GetEquipQualityType().ToString() + StrTextJsonData.DataDict["EquipPingji" + this.Quality].ChinaText;
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x001D4814 File Offset: 0x001D2A14
		public int GetCd()
		{
			int num = 1;
			int value = EquipSeidJsonData2.DataDict[this.Id].value1;
			if (SkillSeidJsonData29.DataDict.ContainsKey(value))
			{
				num = SkillSeidJsonData29.DataDict[value].value1;
			}
			else
			{
				num = EquipItem.GetItemCD(this.Seid, num);
			}
			return num;
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x001D4868 File Offset: 0x001D2A68
		public string GetShuXing()
		{
			int value = EquipSeidJsonData2.DataDict[this.Id].value1;
			List<int> list = _skillJsonData.DataDict[value].AttackType;
			string text = "";
			if (this.Seid.HasField("AttackType"))
			{
				list = this.Seid["AttackType"].ToList();
			}
			foreach (int num in list)
			{
				text += Tools.getStr("xibieFight" + num);
			}
			return text;
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x001D4924 File Offset: 0x001D2B24
		public List<int> GetShuXingList()
		{
			List<int> list = new List<int>();
			if (this.Seid.HasField("AttackType"))
			{
				list = this.Seid["AttackType"].ToList();
			}
			else if (_ItemJsonData.DataDict[this.Id].ItemFlag.Count > 0)
			{
				int num = _ItemJsonData.DataDict[this.Id].ItemFlag[0] - 12000;
				if (num >= 0 && num <= 7)
				{
					list.Add(num);
				}
			}
			return list;
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x001D49B0 File Offset: 0x001D2BB0
		public int GetCurNaiJiu()
		{
			if (this.EquipType != EquipType.丹炉 && this.EquipType != EquipType.灵舟)
			{
				Debug.LogError("该装备没有耐久度");
				return 0;
			}
			return this.Seid["NaiJiu"].I;
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x001D49E8 File Offset: 0x001D2BE8
		public int GetMaxNaiJiu()
		{
			int result = 0;
			if (this.EquipType != EquipType.丹炉 && this.EquipType != EquipType.灵舟)
			{
				Debug.LogError("该装备没有耐久度");
				return result;
			}
			if (this.EquipType == EquipType.丹炉)
			{
				result = 100;
			}
			else if (this.EquipType == EquipType.灵舟)
			{
				result = (int)jsonData.instance.LingZhouPinJie[this.Quality.ToString()]["Naijiu"];
			}
			return result;
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x001D4A58 File Offset: 0x001D2C58
		private static int GetItemCD(JSONObject Seid, int oldCD)
		{
			if (Seid == null || !Seid.HasField("SkillSeids"))
			{
				return oldCD;
			}
			return Seid["SkillSeids"].list.Find((JSONObject aa) => aa["id"].I == 29)["value1"].I;
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x001D4ABA File Offset: 0x001D2CBA
		public override void Use()
		{
			SingletonMono<TabUIMag>.Instance.WuPingPanel.AddEquip((EquipItem)base.Clone());
		}

		// Token: 0x04004663 RID: 18019
		public EquipType EquipType;

		// Token: 0x04004664 RID: 18020
		public EquipQuality EquipQuality;
	}
}
