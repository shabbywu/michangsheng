using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003B5 RID: 949
public class UIBiGuanTuPoPanel : TabPanelBase
{
	// Token: 0x06001A52 RID: 6738 RVA: 0x0001675B File Offset: 0x0001495B
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x00016769 File Offset: 0x00014969
	public void RefreshUI()
	{
		this.RefreshInventory();
		this.SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x000E8550 File Offset: 0x000E6750
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

	// Token: 0x06001A55 RID: 6741 RVA: 0x000E8728 File Offset: 0x000E6928
	public void SetAllIconUnSelected()
	{
		int childCount = this.SVContent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.SVContent.GetChild(i).GetComponent<UIIconShow>().SelectedImage.SetActive(false);
		}
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x000E876C File Offset: 0x000E696C
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

	// Token: 0x06001A57 RID: 6743 RVA: 0x000E87F8 File Offset: 0x000E69F8
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

	// Token: 0x06001A58 RID: 6744 RVA: 0x00016781 File Offset: 0x00014981
	public void OnTuPoButtonClick()
	{
		if (this.tmpIcon == null)
		{
			return;
		}
		this.TuPo();
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x000E8958 File Offset: 0x000E6B58
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
					if (_skillTemp.itemId == (int)jsonobject["Skill_ID"].n && _skillTemp.level < 5)
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

	// Token: 0x040015B2 RID: 5554
	public RectTransform SVContent;

	// Token: 0x040015B3 RID: 5555
	public RectTransform RightContent;

	// Token: 0x040015B4 RID: 5556
	public Dropdown Fillter1;

	// Token: 0x040015B5 RID: 5557
	public Dropdown Filter2;

	// Token: 0x040015B6 RID: 5558
	public Text RightTitle;

	// Token: 0x040015B7 RID: 5559
	public Text TuPoXiaoHaoText;

	// Token: 0x040015B8 RID: 5560
	public Scrollbar JieDuanBar;

	// Token: 0x040015B9 RID: 5561
	public Image BtnImage1;

	// Token: 0x040015BA RID: 5562
	public Image BtnImage2;

	// Token: 0x040015BB RID: 5563
	public Material GreyMat;

	// Token: 0x040015BC RID: 5564
	private UIIconShow tmpIcon;

	// Token: 0x040015BD RID: 5565
	public List<Text> DescItem;
}
