using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200028A RID: 650
public class UIBiGuanTuPoPanel : TabPanelBase
{
	// Token: 0x0600177A RID: 6010 RVA: 0x000A1037 File Offset: 0x0009F237
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000A1045 File Offset: 0x0009F245
	public void RefreshUI()
	{
		this.RefreshInventory();
		this.SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x000A1060 File Offset: 0x0009F260
	public void RefreshInventory()
	{
		this.SVContent.DestoryAllChild();
		Avatar player = PlayerEx.Player;
		int value = this.Fillter1.value;
		int value2 = this.Filter2.value;
		foreach (SkillItem skillItem in player.hasStaticSkillList)
		{
			bool flag = false;
			if (skillItem.level < 5)
			{
				GUIPackage.Skill skill = SkillStaticDatebase.instence.Dict[skillItem.itemId][skillItem.level];
				JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[skill.skill_ID.ToString()];
				if (value2 == 0 || skill.SkillQuality == value2)
				{
					if (value == 0)
					{
						flag = true;
					}
					else
					{
						int i = jsonobject["AttackType"].I;
						if (i < 5)
						{
							if (i == value - 1)
							{
								flag = true;
							}
						}
						else if (value == 6)
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					UIIconShow show = Object.Instantiate<GameObject>(UIIconShow.Prefab, this.SVContent).GetComponent<UIIconShow>();
					show.SetStaticSkill(skill);
					show.CanDrag = false;
					int num = Tools.CalcTuPoTime(skill.skill_ID);
					string topoTime = Tools.getStr("tupozhixiayijie").Replace("{X}", (num / 12).ToString()).Replace("{Y}", (num % 12).ToString()).Replace("突破至下一阶需要：", "");
					show.OnClick = delegate(PointerEventData b)
					{
						this.SetTuPo(show, topoTime);
						this.SetAllIconUnSelected();
						show.SelectedImage.SetActive(true);
					};
				}
			}
		}
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000A1238 File Offset: 0x0009F438
	public void SetAllIconUnSelected()
	{
		int childCount = this.SVContent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.SVContent.GetChild(i).GetComponent<UIIconShow>().SelectedImage.SetActive(false);
		}
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x000A127C File Offset: 0x0009F47C
	public void SetNull()
	{
		this.tmpIcon = null;
		this.RightTitle.text = "突破";
		this.TuPoXiaoHaoText.text = "";
		for (int i = 0; i < 5; i++)
		{
			this.DescItem[i].gameObject.SetActive(false);
		}
		this.BtnImage1.material = this.GreyMat;
		this.BtnImage2.material = this.GreyMat;
		this.JieDuanBar.gameObject.SetActive(false);
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x000A1308 File Offset: 0x0009F508
	public void SetTuPo(UIIconShow iconShow, string tupoTime)
	{
		this.RightContent.anchoredPosition = new Vector2(this.RightContent.anchoredPosition.x, 0f);
		this.tmpIcon = iconShow;
		this.RightTitle.text = iconShow.tmpSkill.skill_Name.RemoveNumber();
		this.BtnImage1.material = null;
		this.BtnImage2.material = null;
		this.TuPoXiaoHaoText.text = tupoTime;
		GUIPackage.Skill tmpSkill = this.tmpIcon.tmpSkill;
		this.JieDuanBar.gameObject.SetActive(true);
		this.JieDuanBar.size = (float)(tmpSkill.Skill_Lv - 1) / 4f;
		for (int i = 1; i <= 5; i++)
		{
			this.DescItem[i - 1].gameObject.SetActive(true);
			this.DescItem[i - 1].text = SkillStaticDatebase.instence.Dict[tmpSkill.SkillID][i].skill_Desc;
			if (tmpSkill.Skill_Lv >= i)
			{
				this.DescItem[i - 1].transform.GetChild(1).gameObject.SetActive(true);
			}
			else
			{
				this.DescItem[i - 1].transform.GetChild(1).gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x000A1468 File Offset: 0x0009F668
	public void OnTuPoButtonClick()
	{
		if (this.tmpIcon == null)
		{
			return;
		}
		this.TuPo();
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000A1480 File Offset: 0x0009F680
	public void TuPo()
	{
		Avatar player = PlayerEx.Player;
		Dictionary<int, GUIPackage.Skill> dicSkills = SkillStaticDatebase.instence.dicSkills;
		int skill_ID = this.tmpIcon.tmpSkill.skill_ID;
		JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[skill_ID.ToString()];
		int num = Tools.CalcTuPoTime(skill_ID);
		int num2 = (int)jsonobject["Skill_Lv"].n;
		if (!XiuLian.CheckCanUse(num))
		{
			UIPopTip.Inst.Pop("房间剩余时间不足，无法进行突破", PopTipIconType.叹号);
			return;
		}
		int num3 = (int)jsonobject["Skill_LV"].n;
		if (num2 < dicSkills[skill_ID].Max_level && num2 < jsonData.instance.StaticLVToLevelJsonData[Tools.instance.getPlayer().getLevelType().ToString()]["Max" + num3].I)
		{
			Tools.instance.playFader("正在突破功法...", delegate
			{
				this.RefreshUI();
			});
			player.AddTime(0, num % 12, num / 12);
			JSONObject jsonobject2 = jsonData.instance.StaticSkillJsonData[(skill_ID + 1).ToString()];
			float n = jsonData.instance.WuDaoExBeiLuJson["1"]["tupo"].n;
			float n2 = jsonobject2["Skill_castTime"].n;
			int i = jsonobject2["Skill_ID"].I;
			foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.ItemJsonData)
			{
				if (keyValuePair.Value["type"].I == 4)
				{
					float num4 = 0f;
					if (float.TryParse(keyValuePair.Value["desc"].str, out num4) && (int)num4 == i)
					{
						item.AddWuDao(jsonobject2["Skill_castTime"].I * 30, jsonData.instance.WuDaoExBeiLuJson["1"]["tupo"].n, Tools.JsonListToList(keyValuePair.Value["wuDao"]), 2);
						break;
					}
				}
			}
			using (List<SkillItem>.Enumerator enumerator2 = player.hasStaticSkillList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					SkillItem _skillTemp = enumerator2.Current;
					if (_skillTemp.itemId == jsonobject["Skill_ID"].I && _skillTemp.level < 5)
					{
						_skillTemp.level++;
						int equipindex = 0;
						if (player.equipStaticSkillList.Find(delegate(SkillItem equipInfo)
						{
							if (equipInfo.itemId == _skillTemp.itemId)
							{
								equipindex = equipInfo.itemIndex;
							}
							return equipInfo.itemId == _skillTemp.itemId;
						}) != null)
						{
							player.UnEquipStaticSkill(_skillTemp.itemId);
							player.equipStaticSkill(_skillTemp.itemId, equipindex);
						}
						new StaticSkill(Tools.instance.getStaticSkillKeyByID(_skillTemp.itemId), 0, 5).Puting(player, player, 3);
					}
				}
			}
			player.HP = player.HP_Max;
			PlayTutorial.CheckGongFaTask();
			return;
		}
		if (num2 >= dicSkills[skill_ID].Max_level)
		{
			UIPopTip.Inst.Pop("已到最大等级", PopTipIconType.叹号);
			return;
		}
		string str = jsonData.instance.StaticLVToLevelJsonData[(player.getLevelType() + 1).ToString()]["Name"].Str;
		UIPopTip.Inst.Pop(str + "后方可继续突破", PopTipIconType.叹号);
	}

	// Token: 0x04001235 RID: 4661
	public RectTransform SVContent;

	// Token: 0x04001236 RID: 4662
	public RectTransform RightContent;

	// Token: 0x04001237 RID: 4663
	public Dropdown Fillter1;

	// Token: 0x04001238 RID: 4664
	public Dropdown Filter2;

	// Token: 0x04001239 RID: 4665
	public Text RightTitle;

	// Token: 0x0400123A RID: 4666
	public Text TuPoXiaoHaoText;

	// Token: 0x0400123B RID: 4667
	public Scrollbar JieDuanBar;

	// Token: 0x0400123C RID: 4668
	public Image BtnImage1;

	// Token: 0x0400123D RID: 4669
	public Image BtnImage2;

	// Token: 0x0400123E RID: 4670
	public Material GreyMat;

	// Token: 0x0400123F RID: 4671
	private UIIconShow tmpIcon;

	// Token: 0x04001240 RID: 4672
	public List<Text> DescItem;
}
