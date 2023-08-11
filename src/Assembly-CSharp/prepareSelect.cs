using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using YSGame;

public class prepareSelect : MonoBehaviour
{
	protected int nowIndex;

	public int maxPage = 5;

	public GameObject showObj;

	public selectSkillConfig.selectType selectType;

	public string StartText = "第";

	private void Start()
	{
		Avatar player = Tools.instance.getPlayer();
		switch (selectType)
		{
		case selectSkillConfig.selectType.SelectSkill:
			nowIndex = player.nowConfigEquipSkill;
			break;
		case selectSkillConfig.selectType.SelectStaticSkill:
			nowIndex = player.nowConfigEquipStaticSkill;
			break;
		case selectSkillConfig.selectType.SelectItem:
			nowIndex = player.nowConfigEquipItem;
			break;
		}
		resetObj();
	}

	public virtual void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13);
		addNowPage();
		resetObj();
	}

	public virtual void addNowPage()
	{
		nowIndex++;
		if (nowIndex >= maxPage)
		{
			nowIndex = 0;
		}
	}

	public void setPageTetx()
	{
		UILabel component = ((Component)((Component)this).transform.Find("Label")).GetComponent<UILabel>();
		string text = "";
		Tools.instance.getPlayer();
		text = StartText + (nowIndex + 1).ToCNNumber() + "页";
		component.text = text;
	}

	public virtual void SetFirstPage()
	{
		nowIndex = 0;
		setPageTetx();
	}

	public virtual void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13);
		reduceIndex();
		resetObj();
	}

	public virtual void reduceIndex()
	{
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = maxPage - 1;
		}
	}

	public virtual void resetObj()
	{
		UILabel component = ((Component)((Component)this).transform.Find("Label")).GetComponent<UILabel>();
		string text = "";
		Dictionary<int, GUIPackage.Skill> dicSkills = SkillStaticDatebase.instence.dicSkills;
		Dictionary<int, GUIPackage.Skill> dicSkills2 = SkillDatebase.instence.dicSkills;
		Avatar player = Tools.instance.getPlayer();
		switch (selectType)
		{
		case selectSkillConfig.selectType.SelectSkill:
			text = "技能配置" + (nowIndex + 1);
			player.setSkillConfigIndex(nowIndex);
			break;
		case selectSkillConfig.selectType.SelectStaticSkill:
			text = "功法配置" + (nowIndex + 1);
			player.setStatikConfigIndex(nowIndex);
			break;
		case selectSkillConfig.selectType.SelectItem:
			text = "装备配置" + (nowIndex + 1);
			player.setItemConfigIndex(nowIndex);
			break;
		}
		component.text = text;
		if (selectType == selectSkillConfig.selectType.SelectItem)
		{
			return;
		}
		Transform val = showObj.transform.Find("Key");
		clearSkill(val);
		int num = 0;
		if (selectType == selectSkillConfig.selectType.SelectSkill)
		{
			foreach (SkillItem equipSkill in player.equipSkillList)
			{
				int skillKeyByID = Tools.instance.getSkillKeyByID(equipSkill.itemId, player);
				((Component)val.GetChild(num)).GetComponent<KeyCellMapSkill>().keySkill = dicSkills2[skillKeyByID];
				num++;
			}
			return;
		}
		if (selectType != selectSkillConfig.selectType.SelectStaticSkill)
		{
			return;
		}
		foreach (SkillItem equipStaticSkill in player.equipStaticSkillList)
		{
			int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			if (num < val.childCount)
			{
				((Component)val.GetChild(num)).GetComponent<KeyCellMapPassSkill>().keySkill = dicSkills[staticSkillKeyByID];
				num++;
			}
		}
	}

	public void clearSkill(Transform key)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (Transform item in key)
		{
			Transform val = item;
			if (selectType == selectSkillConfig.selectType.SelectSkill)
			{
				((Component)val).GetComponent<KeyCellMapSkill>().keySkill = new GUIPackage.Skill();
			}
			else if (selectType == selectSkillConfig.selectType.SelectStaticSkill)
			{
				((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill = new GUIPackage.Skill();
			}
		}
	}

	public void addSkill()
	{
	}

	private void Update()
	{
	}
}
