using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x02000294 RID: 660
public class prepareSelect : MonoBehaviour
{
	// Token: 0x06001433 RID: 5171 RVA: 0x000B8D10 File Offset: 0x000B6F10
	private void Start()
	{
		Avatar player = Tools.instance.getPlayer();
		switch (this.selectType)
		{
		case selectSkillConfig.selectType.SelectSkill:
			this.nowIndex = player.nowConfigEquipSkill;
			break;
		case selectSkillConfig.selectType.SelectStaticSkill:
			this.nowIndex = player.nowConfigEquipStaticSkill;
			break;
		case selectSkillConfig.selectType.SelectItem:
			this.nowIndex = player.nowConfigEquipItem;
			break;
		}
		this.resetObj();
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x00012BD3 File Offset: 0x00010DD3
	public virtual void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x00012BF2 File Offset: 0x00010DF2
	public virtual void addNowPage()
	{
		this.nowIndex++;
		if (this.nowIndex >= this.maxPage)
		{
			this.nowIndex = 0;
		}
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000B8D74 File Offset: 0x000B6F74
	public void setPageTetx()
	{
		UILabel component = base.transform.Find("Label").GetComponent<UILabel>();
		Tools.instance.getPlayer();
		string text = this.StartText + (this.nowIndex + 1).ToCNNumber() + "页";
		component.text = text;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x00012C17 File Offset: 0x00010E17
	public virtual void SetFirstPage()
	{
		this.nowIndex = 0;
		this.setPageTetx();
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x00012C26 File Offset: 0x00010E26
	public virtual void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.reduceIndex();
		this.resetObj();
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x00012C45 File Offset: 0x00010E45
	public virtual void reduceIndex()
	{
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.maxPage - 1;
		}
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x000B8DCC File Offset: 0x000B6FCC
	public virtual void resetObj()
	{
		UILabel component = base.transform.Find("Label").GetComponent<UILabel>();
		string text = "";
		Dictionary<int, GUIPackage.Skill> dicSkills = SkillStaticDatebase.instence.dicSkills;
		Dictionary<int, GUIPackage.Skill> dicSkills2 = SkillDatebase.instence.dicSkills;
		Avatar player = Tools.instance.getPlayer();
		switch (this.selectType)
		{
		case selectSkillConfig.selectType.SelectSkill:
			text = "技能配置" + (this.nowIndex + 1);
			player.setSkillConfigIndex(this.nowIndex);
			break;
		case selectSkillConfig.selectType.SelectStaticSkill:
			text = "功法配置" + (this.nowIndex + 1);
			player.setStatikConfigIndex(this.nowIndex);
			break;
		case selectSkillConfig.selectType.SelectItem:
			text = "装备配置" + (this.nowIndex + 1);
			player.setItemConfigIndex(this.nowIndex);
			break;
		}
		component.text = text;
		if (this.selectType != selectSkillConfig.selectType.SelectItem)
		{
			Transform transform = this.showObj.transform.Find("Key");
			this.clearSkill(transform);
			int num = 0;
			if (this.selectType == selectSkillConfig.selectType.SelectSkill)
			{
				using (List<SkillItem>.Enumerator enumerator = player.equipSkillList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SkillItem skillItem = enumerator.Current;
						int skillKeyByID = Tools.instance.getSkillKeyByID(skillItem.itemId, player);
						transform.GetChild(num).GetComponent<KeyCellMapSkill>().keySkill = dicSkills2[skillKeyByID];
						num++;
					}
					return;
				}
			}
			if (this.selectType == selectSkillConfig.selectType.SelectStaticSkill)
			{
				foreach (SkillItem skillItem2 in player.equipStaticSkillList)
				{
					int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(skillItem2.itemId);
					if (num < transform.childCount)
					{
						transform.GetChild(num).GetComponent<KeyCellMapPassSkill>().keySkill = dicSkills[staticSkillKeyByID];
						num++;
					}
				}
			}
		}
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x000B8FE8 File Offset: 0x000B71E8
	public void clearSkill(Transform key)
	{
		foreach (object obj in key)
		{
			Transform transform = (Transform)obj;
			if (this.selectType == selectSkillConfig.selectType.SelectSkill)
			{
				transform.GetComponent<KeyCellMapSkill>().keySkill = new GUIPackage.Skill();
			}
			else if (this.selectType == selectSkillConfig.selectType.SelectStaticSkill)
			{
				transform.GetComponent<KeyCellMapPassSkill>().keySkill = new GUIPackage.Skill();
			}
		}
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x000042DD File Offset: 0x000024DD
	public void addSkill()
	{
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000FAE RID: 4014
	protected int nowIndex;

	// Token: 0x04000FAF RID: 4015
	public int maxPage = 5;

	// Token: 0x04000FB0 RID: 4016
	public GameObject showObj;

	// Token: 0x04000FB1 RID: 4017
	public selectSkillConfig.selectType selectType;

	// Token: 0x04000FB2 RID: 4018
	public string StartText = "第";
}
