using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x0200019C RID: 412
public class prepareSelect : MonoBehaviour
{
	// Token: 0x06001192 RID: 4498 RVA: 0x0006A908 File Offset: 0x00068B08
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

	// Token: 0x06001193 RID: 4499 RVA: 0x0006A969 File Offset: 0x00068B69
	public virtual void nextPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.addNowPage();
		this.resetObj();
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0006A988 File Offset: 0x00068B88
	public virtual void addNowPage()
	{
		this.nowIndex++;
		if (this.nowIndex >= this.maxPage)
		{
			this.nowIndex = 0;
		}
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x0006A9B0 File Offset: 0x00068BB0
	public void setPageTetx()
	{
		UILabel component = base.transform.Find("Label").GetComponent<UILabel>();
		Tools.instance.getPlayer();
		string text = this.StartText + (this.nowIndex + 1).ToCNNumber() + "页";
		component.text = text;
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x0006AA07 File Offset: 0x00068C07
	public virtual void SetFirstPage()
	{
		this.nowIndex = 0;
		this.setPageTetx();
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0006AA16 File Offset: 0x00068C16
	public virtual void lastPage()
	{
		MusicMag.instance.PlayEffectMusic(13, 1f);
		this.reduceIndex();
		this.resetObj();
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0006AA35 File Offset: 0x00068C35
	public virtual void reduceIndex()
	{
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.maxPage - 1;
		}
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0006AA5C File Offset: 0x00068C5C
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

	// Token: 0x0600119A RID: 4506 RVA: 0x0006AC78 File Offset: 0x00068E78
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

	// Token: 0x0600119B RID: 4507 RVA: 0x00004095 File Offset: 0x00002295
	public void addSkill()
	{
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000C9B RID: 3227
	protected int nowIndex;

	// Token: 0x04000C9C RID: 3228
	public int maxPage = 5;

	// Token: 0x04000C9D RID: 3229
	public GameObject showObj;

	// Token: 0x04000C9E RID: 3230
	public selectSkillConfig.selectType selectType;

	// Token: 0x04000C9F RID: 3231
	public string StartText = "第";
}
