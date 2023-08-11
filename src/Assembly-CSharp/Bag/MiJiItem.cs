using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

namespace Bag;

[Serializable]
public class MiJiItem : BaseItem
{
	public MiJiType MiJiType;

	public int CanUse;

	public new int PinJie;

	public override void SetItem(int id, int count)
	{
		base.SetItem(id, count);
		MiJiType = GetMiJiType();
		CanUse = _ItemJsonData.DataDict[id].CanUse;
		PinJie = _ItemJsonData.DataDict[id].typePinJie;
	}

	public override string GetQualityName()
	{
		if (MiJiType == MiJiType.功法 || MiJiType == MiJiType.技能)
		{
			return GetPinJie() + GetPinJieName();
		}
		return base.GetQualityName();
	}

	public override JiaoBiaoType GetJiaoBiaoType()
	{
		if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(Id)) >= CanUse && MiJiType == MiJiType.书籍2)
		{
			return JiaoBiaoType.悟;
		}
		if (MiJiType == MiJiType.丹方)
		{
			if (Tools.instance.getPlayer().ISStudyDanFan(ItemsSeidJsonData13.DataDict[Id].value1))
			{
				return JiaoBiaoType.悟;
			}
		}
		else if (MiJiType == MiJiType.功法)
		{
			int id = 0;
			if (Id > jsonData.QingJiaoItemIDSegment)
			{
				id = ItemsSeidJsonData2.DataDict[Id - jsonData.QingJiaoItemIDSegment].value1;
			}
			else
			{
				id = ItemsSeidJsonData2.DataDict[Id].value1;
			}
			if (PlayerEx.Player.hasStaticSkillList.Find((SkillItem s) => s.itemId == id) != null)
			{
				return JiaoBiaoType.悟;
			}
		}
		else if (MiJiType == MiJiType.技能)
		{
			int id2 = 0;
			if (Id > jsonData.QingJiaoItemIDSegment)
			{
				id2 = ItemsSeidJsonData1.DataDict[Id - jsonData.QingJiaoItemIDSegment].value1;
			}
			else
			{
				id2 = ItemsSeidJsonData1.DataDict[Id].value1;
			}
			if (PlayerEx.Player.hasSkillList.Find((SkillItem s) => s.itemId == id2) != null)
			{
				return JiaoBiaoType.悟;
			}
		}
		return base.GetJiaoBiaoType();
	}

	public override string GetDesc1()
	{
		int result;
		if (MiJiType == MiJiType.功法)
		{
			if (!int.TryParse(_ItemJsonData.DataDict[Id].desc.Replace(".0", ""), out result))
			{
				string text = $"获取描述异常，id为{Id}的功法书无法将描述转换为功法ID，请检查配表";
				Debug.LogError((object)text);
				return text;
			}
			foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
			{
				if (data.Skill_ID == result)
				{
					return data.descr;
				}
			}
		}
		else if (MiJiType == MiJiType.技能)
		{
			if (!int.TryParse(_ItemJsonData.DataDict[Id].desc.Replace(".0", ""), out result))
			{
				string text2 = $"获取描述异常，id为{Id}的技能书无法将描述转换为技能ID，请检查配表";
				Debug.LogError((object)text2);
				return text2;
			}
			foreach (_skillJsonData data2 in _skillJsonData.DataList)
			{
				if (data2.Skill_ID == result && data2.Skill_Lv == Tools.instance.getPlayer().getLevelType())
				{
					return data2.descr.Replace("（attack）", data2.HP.ToString());
				}
			}
		}
		return base.GetDesc1();
	}

	public override List<int> GetCiZhui()
	{
		new List<int>();
		int result;
		if (MiJiType == MiJiType.功法)
		{
			if (int.TryParse(_ItemJsonData.DataDict[Id].desc.Replace(".0", ""), out result))
			{
				foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
				{
					if (data.Skill_ID == result && data.Skill_Lv == Tools.instance.getPlayer().getLevelType())
					{
						return new List<int>(data.Affix);
					}
				}
			}
			else
			{
				Debug.LogError((object)$"获取词缀异常，id为{Id}的功法书无法将描述转换为功法ID，请检查配表");
			}
		}
		else if (MiJiType == MiJiType.技能)
		{
			if (int.TryParse(_ItemJsonData.DataDict[Id].desc.Replace(".0", ""), out result))
			{
				foreach (_skillJsonData data2 in _skillJsonData.DataList)
				{
					if (data2.Skill_ID == result && data2.Skill_Lv == Tools.instance.getPlayer().getLevelType())
					{
						return new List<int>(data2.Affix2);
					}
				}
			}
			else
			{
				Debug.LogError((object)$"获取词缀异常，id为{Id}的技能书无法将描述转换为技能ID，请检查配表");
			}
		}
		return base.GetCiZhui();
	}

	public List<SkillCost> GetMiJiCost()
	{
		int id = int.Parse(_ItemJsonData.DataDict[Id].desc.Replace(".0", ""));
		ActiveSkill activeSkill = new ActiveSkill();
		activeSkill.SetSkill(id, Tools.instance.getPlayer().getLevelType());
		return activeSkill.GetSkillCost();
	}

	public override Sprite GetQualitySprite()
	{
		return BagMag.Inst.QualityDict[GetImgQuality().ToString()];
	}

	public override Sprite GetQualityUpSprite()
	{
		return BagMag.Inst.QualityUpDict[GetImgQuality().ToString()];
	}

	public override int GetImgQuality()
	{
		int num = Quality;
		if (MiJiType == MiJiType.功法 || MiJiType == MiJiType.技能)
		{
			num *= 2;
		}
		return num;
	}

	public MiJiType GetMiJiType()
	{
		MiJiType result = MiJiType.技能;
		switch (Type)
		{
		case 3:
			result = MiJiType.技能;
			break;
		case 4:
			result = MiJiType.功法;
			break;
		case 10:
			result = MiJiType.丹方;
			break;
		case 12:
			result = MiJiType.书籍1;
			break;
		case 13:
			result = MiJiType.书籍2;
			break;
		}
		return result;
	}

	private string GetPinJie()
	{
		return Quality switch
		{
			1 => "人阶", 
			2 => "地阶", 
			3 => "天阶", 
			_ => "无", 
		};
	}

	public string GetPinJieName()
	{
		return PinJie switch
		{
			1 => "下", 
			2 => "中", 
			3 => "上", 
			_ => "无", 
		};
	}

	public override void Use()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		if (MiJiType == MiJiType.书籍1 || MiJiType == MiJiType.丹方)
		{
			new item(Id).gongneng((UnityAction)delegate
			{
				Tools.instance.getPlayer().removeItem(Id, 1);
				MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM);
			});
		}
		else
		{
			UIPopTip.Inst.Pop("需在闭关时领悟");
		}
	}
}
