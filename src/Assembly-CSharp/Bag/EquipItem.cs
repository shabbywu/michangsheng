using System;
using System.Collections.Generic;
using JSONClass;
using Tab;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D26 RID: 3366
	[Serializable]
	public class EquipItem : BaseItem
	{
		// Token: 0x06005026 RID: 20518 RVA: 0x002189BC File Offset: 0x00216BBC
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

		// Token: 0x06005027 RID: 20519 RVA: 0x00039C86 File Offset: 0x00037E86
		public override string GetDesc1()
		{
			if (this.Seid.HasField("SeidDesc"))
			{
				return this.Seid["SeidDesc"].str;
			}
			return base.GetDesc1();
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x00039CB6 File Offset: 0x00037EB6
		public override string GetDesc2()
		{
			if (this.Seid.HasField("Desc"))
			{
				return this.Seid["Desc"].str;
			}
			return base.GetDesc2();
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x00039CE6 File Offset: 0x00037EE6
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.EquipType = this.GetEquipType();
			this.EquipQuality = this.GetEquipQualityType();
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x00218AA4 File Offset: 0x00216CA4
		public override int GetImgQuality()
		{
			int result = this.Quality;
			if (this.EquipType != EquipType.丹炉 && this.EquipType != EquipType.灵舟)
			{
				result = this.Quality + 1;
			}
			return result;
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x00218AD4 File Offset: 0x00216CD4
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

		// Token: 0x0600502C RID: 20524 RVA: 0x00218D0C File Offset: 0x00216F0C
		public override Sprite GetIconSprite()
		{
			if (this.Seid != null && this.Seid.HasField("ItemIcon"))
			{
				return ResManager.inst.LoadSprite(this.Seid["ItemIcon"].str);
			}
			return base.GetIconSprite();
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x002182A8 File Offset: 0x002164A8
		public override Sprite GetQualitySprite()
		{
			return BagMag.Inst.QualityDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x002182D4 File Offset: 0x002164D4
		public override Sprite GetQualityUpSprite()
		{
			return BagMag.Inst.QualityUpDict[this.GetImgQuality().ToString()];
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x00218D5C File Offset: 0x00216F5C
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

		// Token: 0x06005030 RID: 20528 RVA: 0x00039983 File Offset: 0x00037B83
		public override int GetPlayerPrice()
		{
			return (int)((float)this.GetPrice() * 0.5f);
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x00039D08 File Offset: 0x00037F08
		public override string GetName()
		{
			if (this.Seid != null && this.Seid.HasField("Name"))
			{
				return this.Seid["Name"].str;
			}
			return base.GetName();
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x00218DCC File Offset: 0x00216FCC
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

		// Token: 0x06005033 RID: 20531 RVA: 0x00218E14 File Offset: 0x00217014
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

		// Token: 0x06005034 RID: 20532 RVA: 0x00039D40 File Offset: 0x00037F40
		public bool EquipTypeIsEqual(EquipType targetType)
		{
			if (targetType == EquipType.装备)
			{
				return this.EquipType == EquipType.武器 || this.EquipType == EquipType.防具 || this.EquipType == EquipType.饰品;
			}
			return targetType == this.EquipType;
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x00218E4C File Offset: 0x0021704C
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

		// Token: 0x06005036 RID: 20534 RVA: 0x00218ED8 File Offset: 0x002170D8
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

		// Token: 0x06005037 RID: 20535 RVA: 0x00218F2C File Offset: 0x0021712C
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

		// Token: 0x06005038 RID: 20536 RVA: 0x00218FE8 File Offset: 0x002171E8
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

		// Token: 0x06005039 RID: 20537 RVA: 0x00039D6E File Offset: 0x00037F6E
		public int GetCurNaiJiu()
		{
			if (this.EquipType != EquipType.丹炉 && this.EquipType != EquipType.灵舟)
			{
				Debug.LogError("该装备没有耐久度");
				return 0;
			}
			return this.Seid["NaiJiu"].I;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00219074 File Offset: 0x00217274
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

		// Token: 0x0600503B RID: 20539 RVA: 0x002190E4 File Offset: 0x002172E4
		private static int GetItemCD(JSONObject Seid, int oldCD)
		{
			if (Seid == null || !Seid.HasField("SkillSeids"))
			{
				return oldCD;
			}
			return Seid["SkillSeids"].list.Find((JSONObject aa) => aa["id"].I == 29)["value1"].I;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00039DA3 File Offset: 0x00037FA3
		public override void Use()
		{
			SingletonMono<TabUIMag>.Instance.WuPingPanel.AddEquip((EquipItem)base.Clone());
		}

		// Token: 0x04005161 RID: 20833
		public EquipType EquipType;

		// Token: 0x04005162 RID: 20834
		public EquipQuality EquipQuality;
	}
}
