using System;
using System.Collections.Generic;
using JSONClass;
using Tab;
using UnityEngine;

namespace Bag;

[Serializable]
public class EquipItem : BaseItem
{
	public EquipType EquipType;

	public EquipQuality EquipQuality;

	public override void SetItem(int id, int count, JSONObject seid)
	{
		base.SetItem(id, count, seid);
		if (seid.HasField("Name"))
		{
			Name = seid["Name"].Str;
		}
		if (seid.HasField("quality"))
		{
			Quality = seid["quality"].I;
		}
		if (seid.HasField("QPingZhi"))
		{
			PinJie = seid["QPingZhi"].I;
		}
		EquipType = GetEquipType();
		switch (EquipType)
		{
		case EquipType.武器:
			CanPutSlotType = CanSlotType.武器;
			break;
		case EquipType.防具:
			CanPutSlotType = CanSlotType.衣服;
			break;
		case EquipType.饰品:
			CanPutSlotType = CanSlotType.饰品;
			break;
		case EquipType.丹炉:
			CanPutSlotType = CanSlotType.丹炉;
			break;
		case EquipType.灵舟:
			CanPutSlotType = CanSlotType.灵舟;
			break;
		}
		EquipQuality = GetEquipQualityType();
	}

	public override string GetDesc1()
	{
		if (Seid.HasField("SeidDesc"))
		{
			return Seid["SeidDesc"].str;
		}
		return base.GetDesc1();
	}

	public override string GetDesc2()
	{
		if (Seid.HasField("Desc"))
		{
			return Seid["Desc"].str;
		}
		return base.GetDesc2();
	}

	public override void SetItem(int id, int count)
	{
		base.SetItem(id, count);
		EquipType = GetEquipType();
		EquipQuality = GetEquipQualityType();
	}

	public override int GetImgQuality()
	{
		int result = Quality;
		if (EquipType != EquipType.丹炉 && EquipType != EquipType.灵舟)
		{
			result = Quality + 1;
		}
		return result;
	}

	public override List<int> GetCiZhui()
	{
		if (Seid.HasField("SkillSeids") || Seid.HasField("ItemSeids"))
		{
			int num = 0;
			int num2 = 0;
			List<int> list = new List<int>();
			if (EquipType == EquipType.武器)
			{
				List<JSONObject> list2 = Seid["SkillSeids"].list;
				for (int i = 0; i < list2.Count; i++)
				{
					int i2 = list2[i]["id"].I;
					if (i2 != 79 && i2 != 80)
					{
						continue;
					}
					for (int j = 0; j < list2[i]["value1"].Count; j++)
					{
						num = list2[i]["value1"][j].I;
						for (int k = 0; k < _BuffJsonData.DataDict[num].Affix.Count; k++)
						{
							num2 = _BuffJsonData.DataDict[num].Affix[k];
							if (!list.Contains(num2))
							{
								list.Add(num2);
							}
						}
					}
				}
			}
			else
			{
				List<JSONObject> list3 = Seid["ItemSeids"].list;
				for (int l = 0; l < list3.Count; l++)
				{
					int i3 = list3[l]["id"].I;
					if (i3 != 5 && i3 != 17)
					{
						continue;
					}
					for (int m = 0; m < list3[l]["value1"].Count; m++)
					{
						num = list3[l]["value1"][m].I;
						for (int n = 0; n < _BuffJsonData.DataDict[num].Affix.Count; n++)
						{
							num2 = _BuffJsonData.DataDict[num].Affix[n];
							if (!list.Contains(num2))
							{
								list.Add(num2);
							}
						}
					}
				}
			}
			return list;
		}
		return base.GetCiZhui();
	}

	public override Sprite GetIconSprite()
	{
		if (Seid != null && Seid.HasField("ItemIcon"))
		{
			return ResManager.inst.LoadSprite(Seid["ItemIcon"].str);
		}
		return base.GetIconSprite();
	}

	public override Sprite GetQualitySprite()
	{
		return BagMag.Inst.QualityDict[GetImgQuality().ToString()];
	}

	public override Sprite GetQualityUpSprite()
	{
		return BagMag.Inst.QualityUpDict[GetImgQuality().ToString()];
	}

	public override int GetPrice()
	{
		if (Seid != null && Seid.HasField("Money"))
		{
			return Seid["Money"].I;
		}
		if (Seid != null && Seid.HasField("NaiJiu"))
		{
			return (int)((float)base.GetPrice() * getItemNaiJiuPrice());
		}
		return base.GetPrice();
	}

	public override int GetPlayerPrice()
	{
		return (int)((float)GetPrice() * 0.5f);
	}

	public override string GetName()
	{
		if (Seid != null && Seid.HasField("Name"))
		{
			return Seid["Name"].str;
		}
		return base.GetName();
	}

	public EquipType GetEquipType()
	{
		EquipType result = EquipType.武器;
		switch (Type)
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
		case 9:
			result = EquipType.丹炉;
			break;
		case 14:
			result = EquipType.灵舟;
			break;
		}
		return result;
	}

	public EquipQuality GetEquipQualityType()
	{
		EquipQuality result = EquipQuality.下品;
		switch (PinJie)
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

	public bool EquipTypeIsEqual(EquipType targetType)
	{
		if (targetType == EquipType.装备)
		{
			if (EquipType == EquipType.武器 || EquipType == EquipType.防具 || EquipType == EquipType.饰品)
			{
				return true;
			}
			return false;
		}
		return targetType == EquipType;
	}

	public override string GetQualityName()
	{
		if (EquipType == EquipType.丹炉 || EquipType == EquipType.灵舟)
		{
			return base.GetQualityName();
		}
		if (Seid.HasField("qualitydesc"))
		{
			return Seid["qualitydesc"].Str;
		}
		return GetEquipQualityType().ToString() + StrTextJsonData.DataDict["EquipPingji" + Quality].ChinaText;
	}

	public int GetCd()
	{
		int oldCD = 1;
		int value = EquipSeidJsonData2.DataDict[Id].value1;
		if (SkillSeidJsonData29.DataDict.ContainsKey(value))
		{
			return SkillSeidJsonData29.DataDict[value].value1;
		}
		return GetItemCD(Seid, oldCD);
	}

	public string GetShuXing()
	{
		int value = EquipSeidJsonData2.DataDict[Id].value1;
		List<int> list = _skillJsonData.DataDict[value].AttackType;
		string text = "";
		if (Seid.HasField("AttackType"))
		{
			list = Seid["AttackType"].ToList();
		}
		foreach (int item in list)
		{
			text += Tools.getStr("xibieFight" + item);
		}
		return text;
	}

	public List<int> GetShuXingList()
	{
		List<int> list = new List<int>();
		if (Seid.HasField("AttackType"))
		{
			list = Seid["AttackType"].ToList();
		}
		else if (_ItemJsonData.DataDict[Id].ItemFlag.Count > 0)
		{
			int num = _ItemJsonData.DataDict[Id].ItemFlag[0] - 12000;
			if (num >= 0 && num <= 7)
			{
				list.Add(num);
			}
		}
		return list;
	}

	public int GetCurNaiJiu()
	{
		if (EquipType != EquipType.丹炉 && EquipType != EquipType.灵舟)
		{
			Debug.LogError((object)"该装备没有耐久度");
			return 0;
		}
		return Seid["NaiJiu"].I;
	}

	public int GetMaxNaiJiu()
	{
		int result = 0;
		if (EquipType != EquipType.丹炉 && EquipType != EquipType.灵舟)
		{
			Debug.LogError((object)"该装备没有耐久度");
			return result;
		}
		if (EquipType == EquipType.丹炉)
		{
			result = 100;
		}
		else if (EquipType == EquipType.灵舟)
		{
			result = (int)jsonData.instance.LingZhouPinJie[Quality.ToString()][(object)"Naijiu"];
		}
		return result;
	}

	private static int GetItemCD(JSONObject Seid, int oldCD)
	{
		if (Seid == null || !Seid.HasField("SkillSeids"))
		{
			return oldCD;
		}
		return Seid["SkillSeids"].list.Find((JSONObject aa) => aa["id"].I == 29)["value1"].I;
	}

	public override void Use()
	{
		SingletonMono<TabUIMag>.Instance.WuPingPanel.AddEquip((EquipItem)Clone());
	}
}
