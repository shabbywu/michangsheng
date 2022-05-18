using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame;

namespace GUIPackage
{
	// Token: 0x02000D95 RID: 3477
	public class Skill_UIST : MonoBehaviour
	{
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x0022F908 File Offset: 0x0022DB08
		// (set) Token: 0x060053E9 RID: 21481 RVA: 0x0022F8B0 File Offset: 0x0022DAB0
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

		// Token: 0x060053EB RID: 21483 RVA: 0x0022F964 File Offset: 0x0022DB64
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

		// Token: 0x060053EC RID: 21484 RVA: 0x0022FAAC File Offset: 0x0022DCAC
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

		// Token: 0x060053ED RID: 21485 RVA: 0x0022FB1C File Offset: 0x0022DD1C
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

		// Token: 0x060053EE RID: 21486 RVA: 0x0022FBA4 File Offset: 0x0022DDA4
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

		// Token: 0x060053EF RID: 21487 RVA: 0x0022FC28 File Offset: 0x0022DE28
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

		// Token: 0x060053F0 RID: 21488 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		public void Clear_Draged()
		{
			this.dragedSkill = new Skill();
			this.draggingSkill = false;
			this.Temp.GetComponent<UITexture>().mainTexture = null;
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0022FF54 File Offset: 0x0022E154
		public void SkillUP(int id)
		{
			this.UPSkillUI.SetActive(true);
			this.UPSkillUI.transform.localPosition = Vector3.zero;
			this.UPSkillUI.transform.localScale = Vector3.one;
			this.UPSkillUI.GetComponent<UI_UPSkill>().initSkill(Tools.instance.getStaticSkillIDByKey(this.skill[id].skill_ID));
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0003BF92 File Offset: 0x0003A192
		public void UseSkill(ref Skill S)
		{
			((Avatar)KBEngineApp.app.player()).spell.spellSkill(S.skill_ID, "");
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x0022FFC4 File Offset: 0x0022E1C4
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

		// Token: 0x060053F4 RID: 21492 RVA: 0x00230008 File Offset: 0x0022E208
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
					if ((int)keyValuePair.Value["type"].n == 4 && (int)jsonobject["Skill_ID"].n == (int)float.Parse(keyValuePair.Value["desc"].str))
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

		// Token: 0x060053F5 RID: 21493 RVA: 0x002303F4 File Offset: 0x0022E5F4
		public void SaveSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				PlayerPrefs.SetInt("Skill Level" + i, this.skill[i].skill_level);
			}
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00230440 File Offset: 0x0022E640
		public void LoadSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				this.skill[i].skill_level = YSSaveGame.GetInt("Skill Level" + i, 0);
			}
		}

		// Token: 0x040053A4 RID: 21412
		public List<Skill> skill = new List<Skill>();

		// Token: 0x040053A5 RID: 21413
		private SkillStaticDatebase datebase;

		// Token: 0x040053A6 RID: 21414
		public GameObject skillWin;

		// Token: 0x040053A7 RID: 21415
		public GameObject Tooltip;

		// Token: 0x040053A8 RID: 21416
		public bool draggingSkill;

		// Token: 0x040053A9 RID: 21417
		public Skill dragedSkill;

		// Token: 0x040053AA RID: 21418
		private bool Show_Skill;

		// Token: 0x040053AB RID: 21419
		public GameObject Temp;

		// Token: 0x040053AC RID: 21420
		public GameObject UIGrid;

		// Token: 0x040053AD RID: 21421
		public GameObject UPSkillUI;

		// Token: 0x040053AE RID: 21422
		public GameObject SkillTemp;

		// Token: 0x040053AF RID: 21423
		public int nowIndex;

		// Token: 0x040053B0 RID: 21424
		public int ShowType;

		// Token: 0x040053B1 RID: 21425
		public int OnePageNum;

		// Token: 0x040053B2 RID: 21426
		public selectStaticSkill selectpage;

		// Token: 0x040053B3 RID: 21427
		public bool showCellName;

		// Token: 0x040053B4 RID: 21428
		public bool showDengjie;

		// Token: 0x040053B5 RID: 21429
		public int showLeixing;
	}
}
