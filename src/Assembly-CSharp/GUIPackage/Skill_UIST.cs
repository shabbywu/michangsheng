using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame;

namespace GUIPackage
{
	// Token: 0x02000A69 RID: 2665
	public class Skill_UIST : MonoBehaviour
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x001FD870 File Offset: 0x001FBA70
		// (set) Token: 0x06004ADF RID: 19167 RVA: 0x001FD818 File Offset: 0x001FBA18
		public bool showTooltip
		{
			get
			{
				bool result = false;
				if (this.Tooltip.GetComponent<TooltipScale>() != null)
				{
					result = this.Tooltip.GetComponent<TooltipScale>().showTooltip;
				}
				else if (this.Tooltip.GetComponent<TooltipItem>())
				{
					result = this.Tooltip.GetComponent<TooltipItem>().showTooltip;
				}
				return result;
			}
			set
			{
				if (this.Tooltip.GetComponent<TooltipScale>() != null)
				{
					this.Tooltip.GetComponent<TooltipScale>().showTooltip = value;
					return;
				}
				if (this.Tooltip.GetComponent<TooltipItem>())
				{
					this.Tooltip.GetComponent<TooltipItem>().showTooltip = value;
				}
			}
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x001FD8CC File Offset: 0x001FBACC
		private void Awake()
		{
			this.datebase = jsonData.instance.gameObject.GetComponent<SkillStaticDatebase>();
			if (this.SkillTemp == null)
			{
				this.SkillTemp = (Resources.Load("StaticSkill") as GameObject);
			}
			if (this.OnePageNum == 0)
			{
				this.OnePageNum = 30;
			}
			for (int i = 0; i < this.OnePageNum; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.SkillTemp);
				gameObject.SetActive(true);
				if (gameObject.GetComponent<SkillStaticCell>() != null)
				{
					gameObject.GetComponent<SkillStaticCell>().skillID = -1;
					gameObject.GetComponent<SkillStaticCell>().skill_UIST = this;
					gameObject.GetComponent<SkillStaticCell>().showName = this.showCellName;
					gameObject.GetComponent<SkillStaticCell>().showDengji = this.showDengjie;
				}
				else if (gameObject.GetComponent<StaticTuPoCell>() != null)
				{
					gameObject.GetComponent<StaticTuPoCell>().skillID = -1;
					gameObject.GetComponent<SkillStaticCell>().skill_UIST = this;
				}
				gameObject.transform.parent = this.skillWin.transform.Find("UIGrid").transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			base.GetComponentInChildren<UIGrid>().repositionNow = true;
			this.initSkill_UI();
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x001FDA14 File Offset: 0x001FBC14
		private void Update()
		{
			if (this.draggingSkill)
			{
				if (Input.GetMouseButtonUp(0))
				{
					this.Clear_Draged();
				}
				this.Temp.transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
				this.Temp.GetComponent<UITexture>().mainTexture = this.dragedSkill.skill_Icon;
				Singleton.skillUI2.showTooltip = false;
			}
			this.skillCD();
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x001FDA84 File Offset: 0x001FBC84
		private void skillCD()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				if (this.skill[i].CurCD != 0f)
				{
					this.skill[i].CurCD -= Time.deltaTime;
					if (this.skill[i].CurCD <= 0f)
					{
						this.skill[i].CurCD = 0f;
					}
				}
			}
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x001FDB0C File Offset: 0x001FBD0C
		private void Show()
		{
			this.Show_Skill = !this.Show_Skill;
			if (!this.Show_Skill)
			{
				Singleton.inventory.showTooltip = false;
			}
			if (this.Show_Skill)
			{
				base.transform.Find("Win").position = base.transform.position;
			}
			this.skillWin.SetActive(this.Show_Skill);
			Singleton.UI.UI_Top(this.skillWin.transform.parent);
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x001FDB90 File Offset: 0x001FBD90
		private void initSkill_UI()
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (avatar == null)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			List<int> list = new List<int>
			{
				0,
				1,
				2,
				3,
				4
			};
			foreach (SkillItem skillItem in avatar.hasStaticSkillList)
			{
				if (!this.datebase.Dict.ContainsKey(skillItem.itemId))
				{
					Debug.LogError(string.Format("初始化技能UI出错，技能数据库datebase.Dict不存在技能ID {0}", skillItem.itemId));
				}
				else if (!this.datebase.Dict[skillItem.itemId].ContainsKey(skillItem.level))
				{
					Debug.LogError(string.Format("初始化技能UI出错，技能数据库datebase.Dict[{0}]不存在技能等级 {1}", skillItem.itemId, skillItem.level));
				}
				else
				{
					Skill skill = this.datebase.Dict[skillItem.itemId][skillItem.level];
					int skill_ID = skill.skill_ID;
					JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[string.Concat(skill_ID)];
					bool flag = false;
					if (this.showLeixing == 6 && !list.Contains(jsonobject["AttackType"].I))
					{
						flag = true;
					}
					if ((this.showLeixing == 0 || (this.showLeixing != 6 && jsonobject["AttackType"].I == this.showLeixing - 1) || flag) && (this.ShowType == 0 || jsonobject["Skill_LV"].I == this.ShowType))
					{
						if (num2 >= this.nowIndex * this.OnePageNum && num2 < (this.nowIndex + 1) * this.OnePageNum)
						{
							this.skill.Add(skill);
							GameObject gameObject = this.skillWin.transform.Find("UIGrid").GetChild(num).gameObject;
							if (gameObject.GetComponent<SkillStaticCell>() != null)
							{
								gameObject.GetComponent<SkillStaticCell>().skillID = num;
							}
							else if (gameObject.GetComponent<StaticTuPoCell>() != null)
							{
								gameObject.GetComponent<StaticTuPoCell>().skillID = num;
							}
							num++;
						}
						num2++;
					}
				}
			}
			for (int i = num; i < this.OnePageNum; i++)
			{
				GameObject gameObject2 = this.skillWin.transform.Find("UIGrid").GetChild(num).gameObject;
				if (gameObject2.GetComponent<SkillStaticCell>() != null)
				{
					gameObject2.GetComponent<SkillStaticCell>().skillID = -1;
				}
				else if (gameObject2.GetComponent<StaticTuPoCell>() != null)
				{
					gameObject2.GetComponent<StaticTuPoCell>().skillID = -1;
				}
			}
			if (this.selectpage != null)
			{
				this.selectpage.maxPage = num2 / this.OnePageNum + 1;
			}
			this.draggingSkill = false;
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x001FDEBC File Offset: 0x001FC0BC
		public void Clear_Draged()
		{
			this.dragedSkill = new Skill();
			this.draggingSkill = false;
			this.Temp.GetComponent<UITexture>().mainTexture = null;
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x001FDEE4 File Offset: 0x001FC0E4
		public void SkillUP(int id)
		{
			this.UPSkillUI.SetActive(true);
			this.UPSkillUI.transform.localPosition = Vector3.zero;
			this.UPSkillUI.transform.localScale = Vector3.one;
			this.UPSkillUI.GetComponent<UI_UPSkill>().initSkill(Tools.instance.getStaticSkillIDByKey(this.skill[id].skill_ID));
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x001FD240 File Offset: 0x001FB440
		public void UseSkill(ref Skill S)
		{
			((Avatar)KBEngineApp.app.player()).spell.spellSkill(S.skill_ID, "");
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x001FDF54 File Offset: 0x001FC154
		public int GetSkillID(int id)
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				if (Tools.instance.getStaticSkillIDByKey(this.skill[i].skill_ID) == id)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x001FDF98 File Offset: 0x001FC198
		public void Show_Tooltip(Skill _skill, int STSKillType = 0)
		{
			int skill_ID = _skill.skill_ID;
			TooltipItem component = this.Tooltip.GetComponent<TooltipItem>();
			component.Clear();
			JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[skill_ID.ToString()];
			component.Label1.text = "[E0DDB4]" + _skill.skill_Desc;
			int num = (int)jsonobject["Skill_LV"].n * 2;
			string newValue = jsonData.instance.TootipItemQualityColor[num - 1] + Tools.getStr("pingjie" + (int)jsonobject["Skill_LV"].n) + Tools.getStr("shangzhongxia" + (int)jsonobject["typePinJie"].n);
			component.Label3.text = Tools.getStr("pingjieCell").Replace("{X}", newValue).Replace("[333333]品级：", "");
			component.Label4.text = ((jsonData.instance.TootipItemNameColor[num - 1] + Tools.instance.getStaticSkillName(skill_ID, false)) ?? "");
			string str = Tools.getStr("gongfaleibie" + (int)jsonobject["AttackType"].n);
			component.Label5.text = str;
			component.icon.mainTexture = _skill.skill_Icon;
			if (component.TooltipHelp != null)
			{
				foreach (object obj in component.TooltipHelp.transform.parent)
				{
					Transform transform = (Transform)obj;
					if (transform.gameObject.activeSelf)
					{
						Object.Destroy(transform.gameObject);
					}
				}
			}
			if (STSKillType == 1 && component.TooltipHelp != null)
			{
				JSONObject gongFaBookItem = item.getGongFaBookItem(jsonobject["Skill_ID"].I);
				List<int> wudaoTypeList = new List<int>();
				List<int> wudaoLvList = new List<int>();
				item.GetWuDaoType(gongFaBookItem["id"].I, wudaoTypeList, wudaoLvList);
				string desc = "[d3b068]悟道提升：[-]" + item.StudyTiSheng(wudaoTypeList, "突破后能够提升");
				component.ShowSkillTime(desc);
			}
			component.setCenterTextTitle("【进度】", "【修炼速度】", "");
			component.Label7.text = "第" + Tools.getStr("cengshu" + (int)jsonobject["Skill_Lv"].n);
			component.Label8.text = (int)jsonobject["Skill_Speed"].n + "/月";
			try
			{
				bool flag = false;
				foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.ItemJsonData)
				{
					if ((int)keyValuePair.Value["type"].n == 4 && jsonobject["Skill_ID"].I == (int)float.Parse(keyValuePair.Value["desc"].str))
					{
						component.Label2.text = "[E0DDB4]" + Tools.instance.Code64ToString(keyValuePair.Value["desc2"].str);
						flag = true;
						return;
					}
				}
				if (!flag)
				{
					component.Label2.text = "[E0DDB4]暂无说明[-]";
				}
			}
			catch (Exception)
			{
				component.Label2.text = "[E0DDB4]暂无说明[-]";
			}
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x001FE384 File Offset: 0x001FC584
		public void SaveSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				PlayerPrefs.SetInt("Skill Level" + i, this.skill[i].skill_level);
			}
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x001FE3D0 File Offset: 0x001FC5D0
		public void LoadSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				this.skill[i].skill_level = YSSaveGame.GetInt("Skill Level" + i, 0);
			}
		}

		// Token: 0x04004A07 RID: 18951
		public List<Skill> skill = new List<Skill>();

		// Token: 0x04004A08 RID: 18952
		private SkillStaticDatebase datebase;

		// Token: 0x04004A09 RID: 18953
		public GameObject skillWin;

		// Token: 0x04004A0A RID: 18954
		public GameObject Tooltip;

		// Token: 0x04004A0B RID: 18955
		public bool draggingSkill;

		// Token: 0x04004A0C RID: 18956
		public Skill dragedSkill;

		// Token: 0x04004A0D RID: 18957
		private bool Show_Skill;

		// Token: 0x04004A0E RID: 18958
		public GameObject Temp;

		// Token: 0x04004A0F RID: 18959
		public GameObject UIGrid;

		// Token: 0x04004A10 RID: 18960
		public GameObject UPSkillUI;

		// Token: 0x04004A11 RID: 18961
		public GameObject SkillTemp;

		// Token: 0x04004A12 RID: 18962
		public int nowIndex;

		// Token: 0x04004A13 RID: 18963
		public int ShowType;

		// Token: 0x04004A14 RID: 18964
		public int OnePageNum;

		// Token: 0x04004A15 RID: 18965
		public selectStaticSkill selectpage;

		// Token: 0x04004A16 RID: 18966
		public bool showCellName;

		// Token: 0x04004A17 RID: 18967
		public bool showDengjie;

		// Token: 0x04004A18 RID: 18968
		public int showLeixing;
	}
}
