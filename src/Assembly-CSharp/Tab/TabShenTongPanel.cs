using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using UnityEngine;

namespace Tab;

[Serializable]
public class TabShenTongPanel : ITabPanelBase
{
	private bool _isInit;

	public Dictionary<int, ActiveSkillSlot> AciveSkillDict = new Dictionary<int, ActiveSkillSlot>();

	private Avatar player;

	private FangAnData FangAnData = Tools.instance.getPlayer().StreamData.FangAnData;

	public TabShenTongPanel(GameObject gameObject)
	{
		_go = gameObject;
		_isInit = false;
		player = Tools.instance.getPlayer();
	}

	private void Init()
	{
		Transform transform = Get("SkillList").transform;
		ActiveSkillSlot activeSkillSlot = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			activeSkillSlot = ((Component)transform.GetChild(i)).GetComponent<ActiveSkillSlot>();
			AciveSkillDict.Add(i, activeSkillSlot);
		}
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		SingletonMono<TabUIMag>.Instance.TabBag.OpenBag(BagType.技能);
		LoadSkillData();
		_go.SetActive(true);
	}

	public void LoadSkillData()
	{
		RemoveAll();
		foreach (SkillItem equipSkill in player.equipSkillList)
		{
			BaseSkill baseSkill = new ActiveSkill();
			baseSkill.SetSkill(equipSkill.itemId, Tools.instance.getPlayer().getLevelType());
			AciveSkillDict[equipSkill.itemIndex].SetSlotData(baseSkill);
		}
	}

	public void AddSkill(int index, BaseSkill baseSkill)
	{
		if (AciveSkillDict.ContainsKey(index))
		{
			AciveSkillDict[index].SetSlotData(baseSkill);
			player.equipSkill(baseSkill.SkillId, index);
		}
		else
		{
			Debug.LogError((object)$"不存在当前Key{index}");
		}
	}

	public void ExSkill(int index1, int index2)
	{
		if (AciveSkillDict[index2].IsNull())
		{
			AciveSkillDict[index2].SetSlotData(AciveSkillDict[index1].Skill.Clone());
			RemoveSkill(index1);
		}
		else
		{
			BaseSkill slotData = AciveSkillDict[index1].Skill.Clone();
			BaseSkill slotData2 = AciveSkillDict[index2].Skill.Clone();
			RemoveSkill(index1);
			RemoveSkill(index2);
			AciveSkillDict[index1].SetSlotData(slotData2);
			AciveSkillDict[index2].SetSlotData(slotData);
			player.equipSkill(AciveSkillDict[index1].Skill.SkillId, index1);
		}
		player.equipSkill(AciveSkillDict[index2].Skill.SkillId, index2);
	}

	public bool CanAddSkill(BaseSkill baseSkill)
	{
		foreach (SkillItem equipSkill in player.equipSkillList)
		{
			if (baseSkill.SkillId == equipSkill.itemId)
			{
				return false;
			}
		}
		return true;
	}

	public void RemoveSkill(int index)
	{
		player.UnEquipSkill(AciveSkillDict[index].Skill.SkillId);
		AciveSkillDict[index].SetNull();
	}

	public void RemoveAll()
	{
		foreach (int key in AciveSkillDict.Keys)
		{
			AciveSkillDict[key].SetNull();
		}
	}

	public SlotBase GetNullSlot()
	{
		foreach (int key in AciveSkillDict.Keys)
		{
			if (AciveSkillDict[key].IsNull())
			{
				return AciveSkillDict[key];
			}
		}
		return null;
	}
}
