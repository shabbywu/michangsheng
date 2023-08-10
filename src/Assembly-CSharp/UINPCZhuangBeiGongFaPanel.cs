using System.Collections.Generic;
using UnityEngine;

public class UINPCZhuangBeiGongFaPanel : TabPanelBase
{
	private UINPCData npc;

	public GameObject Equip3Root;

	public GameObject Equip4Root;

	public List<UIIconShow> Equip3IconList = new List<UIIconShow>();

	public List<UIIconShow> Equip4IconList = new List<UIIconShow>();

	public List<UIIconShow> StaticSkillIconList = new List<UIIconShow>();

	public List<UIIconShow> SkillIconList = new List<UIIconShow>();

	public static bool SetNullOnce;

	public override void OnPanelShow()
	{
		base.OnPanelShow();
		if (SetNullOnce)
		{
			SetNullOnce = false;
			SetNull();
			return;
		}
		npc = UINPCJiaoHu.Inst.InfoPanel.npc;
		RefreshEquip();
		RefreshStaticSkill();
		RefreshSkill();
	}

	public void SetNull()
	{
		Equip3Root.SetActive(true);
		Equip4Root.SetActive(false);
		Equip3IconList[0].SetNull();
		Equip3IconList[1].SetNull();
		Equip3IconList[2].SetNull();
		for (int i = 0; i < 6; i++)
		{
			StaticSkillIconList[i].SetNull();
		}
		for (int j = 0; j < 10; j++)
		{
			SkillIconList[j].SetNull();
		}
	}

	private void RefreshEquip()
	{
		if (npc.IsDoubleWeapon)
		{
			Equip3Root.SetActive(false);
			Equip4Root.SetActive(true);
			Equip4IconList[0].SetItem(npc.Weapon1);
			Equip4IconList[1].SetItem(npc.Weapon2);
			if (npc.Clothing != null)
			{
				Equip4IconList[2].SetItem(npc.Clothing);
			}
			else
			{
				Equip4IconList[2].SetNull();
			}
			if (npc.Ring != null)
			{
				Equip4IconList[3].SetItem(npc.Ring);
			}
			else
			{
				Equip4IconList[3].SetNull();
			}
			return;
		}
		Equip3Root.SetActive(true);
		Equip4Root.SetActive(false);
		if (npc.Weapon1 != null)
		{
			Equip3IconList[0].SetItem(npc.Weapon1);
		}
		else
		{
			Equip3IconList[0].SetNull();
		}
		if (npc.Clothing != null)
		{
			Equip3IconList[1].SetItem(npc.Clothing);
		}
		else
		{
			Equip3IconList[1].SetNull();
		}
		if (npc.Ring != null)
		{
			Equip3IconList[2].SetItem(npc.Ring);
		}
		else
		{
			Equip3IconList[2].SetNull();
		}
	}

	private void RefreshStaticSkill()
	{
		for (int i = 0; i < 5; i++)
		{
			if (i >= npc.StaticSkills.Count)
			{
				StaticSkillIconList[i].SetNull();
			}
			else
			{
				StaticSkillIconList[i].SetStaticSkill(npc.StaticSkills[i]);
			}
		}
		if (npc.YuanYingStaticSkill > 0)
		{
			StaticSkillIconList[5].SetStaticSkill(npc.YuanYingStaticSkill);
		}
		else
		{
			StaticSkillIconList[5].SetNull();
		}
	}

	private void RefreshSkill()
	{
		for (int i = 0; i < 10; i++)
		{
			if (i >= npc.Skills.Count)
			{
				SkillIconList[i].SetNull();
			}
			else
			{
				SkillIconList[i].SetSkill(npc.Skills[i]);
			}
		}
	}
}
