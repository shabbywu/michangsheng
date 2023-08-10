using System.Collections.Generic;
using UnityEngine;

namespace KBEngine;

public class SkillBox
{
	public static SkillBox inst;

	public List<Skill> skills = new List<Skill>();

	public Dictionary<string, GameObject> dictSkillDisplay = new Dictionary<string, GameObject>();

	public SkillBox()
	{
		inst = this;
	}

	public void initSkillDisplay()
	{
	}

	public void pull()
	{
		clear();
		KBEngineApp.app.player()?.cellCall("requestPull");
	}

	public void clear()
	{
		skills.Clear();
	}

	public void add(Skill skill)
	{
		for (int i = 0; i < skills.Count; i++)
		{
			if (skills[i].id == skill.id)
			{
				Dbg.DEBUG_MSG("SkillBox::add: " + skill.id + " is exist!");
				return;
			}
		}
		skills.Add(skill);
	}

	public void remove(int id)
	{
		for (int i = 0; i < skills.Count; i++)
		{
			if (skills[i].id == id)
			{
				skills.RemoveAt(i);
				break;
			}
		}
	}

	public int findBoxId(int skillid)
	{
		for (int i = 0; i < skills.Count; i++)
		{
			if (skills[i].id == skillid)
			{
				return i;
			}
		}
		return -1;
	}

	public void showUpdateCD(int skillid)
	{
		int num = findBoxId(skillid);
		if (num != -1)
		{
			((Component)UI_MainUI.inst.btnAll[num]).gameObject.GetComponent<updateCD>().OnBtnClickSkill();
		}
	}

	public Skill get(int id)
	{
		for (int i = 0; i < skills.Count; i++)
		{
			if (skills[i].id == id)
			{
				return skills[i];
			}
		}
		Skill skill = new Skill();
		skill.id = id;
		skill.name = id + " ";
		skill.displayType = (Skill_DisplayType)jsonData.instance.skillJsonData[string.Concat(id)]["Skill_DisplayType"].n;
		skill.canUseDistMax = jsonData.instance.skillJsonData[string.Concat(id)]["canUseDistMax"].n;
		skill.skillEffect = jsonData.instance.skillJsonData[string.Concat(id)]["skillEffect"].str;
		skill.name = jsonData.instance.skillJsonData[string.Concat(id)]["name"].str;
		skill.coolTime = jsonData.instance.skillJsonData[string.Concat(id)]["CD"].n;
		inst.add(skill);
		return skill;
	}
}
