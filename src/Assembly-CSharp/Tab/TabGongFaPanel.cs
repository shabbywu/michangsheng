using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;

namespace Tab;

[Serializable]
public class TabGongFaPanel : ITabPanelBase
{
	private bool _isInit;

	public Dictionary<int, PasstiveSkillSlot> PasstiveSkillDict = new Dictionary<int, PasstiveSkillSlot>();

	private Avatar player;

	public TabGongFaPanel(GameObject gameObject)
	{
		_go = gameObject;
		_isInit = false;
		player = Tools.instance.getPlayer();
	}

	private void Init()
	{
		Transform transform = Get("SkillList").transform;
		PasstiveSkillSlot passtiveSkillSlot = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			passtiveSkillSlot = ((Component)transform.GetChild(i)).GetComponent<PasstiveSkillSlot>();
			PasstiveSkillDict.Add((int)passtiveSkillSlot.SkillSlotType, passtiveSkillSlot);
		}
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		SingletonMono<TabUIMag>.Instance.TabBag.OpenBag(BagType.功法);
		if (player.getLevelType() <= 3)
		{
			((Component)PasstiveSkillDict[6]).gameObject.SetActive(false);
		}
		else
		{
			((Component)PasstiveSkillDict[6]).gameObject.SetActive(true);
		}
		LoadSkillData();
		_go.SetActive(true);
	}

	public void LoadSkillData()
	{
		RemoveAll();
		foreach (SkillItem equipStaticSkill in player.equipStaticSkillList)
		{
			BaseSkill baseSkill = new PassiveSkill();
			baseSkill.SetSkill(equipStaticSkill.itemId, GetGongFaLevel(equipStaticSkill.itemId));
			PasstiveSkillDict[equipStaticSkill.itemIndex].SetSlotData(baseSkill);
		}
	}

	public int GetGongFaLevel(int skillId)
	{
		int result = 1;
		foreach (SkillItem hasStaticSkill in player.hasStaticSkillList)
		{
			if (hasStaticSkill.itemId == skillId)
			{
				result = hasStaticSkill.level;
				break;
			}
		}
		return result;
	}

	public bool CanAddSkill(BaseSkill baseSkill)
	{
		foreach (SkillItem equipStaticSkill in player.equipStaticSkillList)
		{
			if (baseSkill.SkillId == equipStaticSkill.itemId)
			{
				return false;
			}
		}
		return true;
	}

	public int GetSameSkillIndex(BaseSkill baseSkill)
	{
		foreach (int key in PasstiveSkillDict.Keys)
		{
			if (!PasstiveSkillDict[key].IsNull() && PasstiveSkillDict[key].Skill.SkillId == baseSkill.SkillId)
			{
				return key;
			}
		}
		return -1;
	}

	public void AddSkill(GongFaSlotType slotType, BaseSkill baseSkill, int index2 = -1)
	{
		if (PasstiveSkillDict.ContainsKey((int)slotType))
		{
			player.equipStaticSkill(baseSkill.SkillId, (int)slotType);
			PasstiveSkillDict[(int)slotType].SetSlotData(baseSkill);
			if (index2 != -1 && index2 != (int)slotType)
			{
				PasstiveSkillDict[index2].SetNull();
			}
		}
		else
		{
			Debug.LogError((object)$"不存在当前Key{(int)slotType}");
		}
	}

	public void ExSkill(GongFaSlotType slotType1, GongFaSlotType slotType2)
	{
		if (PasstiveSkillDict[(int)slotType2].IsNull())
		{
			PasstiveSkillDict[(int)slotType2].SetSlotData(PasstiveSkillDict[(int)slotType1].Skill.Clone());
			RemoveSkill(slotType1);
		}
		else
		{
			BaseSkill slotData = PasstiveSkillDict[(int)slotType1].Skill.Clone();
			BaseSkill slotData2 = PasstiveSkillDict[(int)slotType2].Skill.Clone();
			RemoveSkill(slotType1);
			RemoveSkill(slotType2);
			PasstiveSkillDict[(int)slotType1].SetSlotData(slotData2);
			PasstiveSkillDict[(int)slotType2].SetSlotData(slotData);
			player.equipStaticSkill(PasstiveSkillDict[(int)slotType1].Skill.SkillId, (int)slotType1);
		}
		player.equipStaticSkill(PasstiveSkillDict[(int)slotType2].Skill.SkillId, (int)slotType2);
	}

	public void RemoveSkill(GongFaSlotType slotType)
	{
		if (PasstiveSkillDict.ContainsKey((int)slotType))
		{
			player.UnEquipStaticSkill(PasstiveSkillDict[(int)slotType].Skill.SkillId);
			PasstiveSkillDict[(int)slotType].SetNull();
		}
		else
		{
			Debug.LogError((object)$"不存在当前Key{(int)slotType}");
		}
	}

	public void RemoveAll()
	{
		foreach (int key in PasstiveSkillDict.Keys)
		{
			PasstiveSkillDict[key].SetNull();
		}
	}

	public SlotBase GetNullSlot()
	{
		foreach (int key in PasstiveSkillDict.Keys)
		{
			if (PasstiveSkillDict[key].IsNull())
			{
				return PasstiveSkillDict[key];
			}
		}
		return null;
	}
}
