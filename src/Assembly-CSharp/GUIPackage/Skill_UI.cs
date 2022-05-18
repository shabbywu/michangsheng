using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

namespace GUIPackage
{
	// Token: 0x02000D94 RID: 3476
	public class Skill_UI : MonoBehaviour
	{
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x0022ED64 File Offset: 0x0022CF64
		// (set) Token: 0x060053D8 RID: 21464 RVA: 0x0022ED0C File Offset: 0x0022CF0C
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

		// Token: 0x060053DA RID: 21466 RVA: 0x0022EDC0 File Offset: 0x0022CFC0
		private void Awake()
		{
			this.datebase = jsonData.instance.gameObject.GetComponent<SkillDatebase>();
			if (this.SkillTemp == null)
			{
				this.SkillTemp = (Resources.Load("skill") as GameObject);
			}
			for (int i = 0; i < this.SkillNum; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.SkillTemp);
				gameObject.SetActive(true);
				gameObject.GetComponent<SkillCell>().skillID = -1;
				gameObject.GetComponent<SkillCell>().showName = this.showCellName;
				gameObject.transform.parent = this.skillWin.transform.Find("UIGrid").transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			base.GetComponentInChildren<UIGrid>().repositionNow = true;
			this.initSkill_UI();
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0022EE9C File Offset: 0x0022D09C
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
				Singleton.skillUI.showTooltip = false;
			}
			this.skillCD();
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0022EF0C File Offset: 0x0022D10C
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

		// Token: 0x060053DD RID: 21469 RVA: 0x0022EF94 File Offset: 0x0022D194
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

		// Token: 0x060053DE RID: 21470 RVA: 0x000042DD File Offset: 0x000024DD
		public void ShowSkillType()
		{
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0022F018 File Offset: 0x0022D218
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
			int levelType = avatar.getLevelType();
			foreach (SkillItem skillItem in avatar.hasSkillList)
			{
				if (!this.datebase.Dict.ContainsKey(skillItem.itemId))
				{
					Debug.LogError(string.Format("初始化技能UI出错，技能数据库datebase.Dict不存在技能ID {0}", skillItem.itemId));
				}
				else if (!this.datebase.Dict[skillItem.itemId].ContainsKey(levelType))
				{
					Debug.LogError(string.Format("初始化技能UI出错，技能数据库datebase.Dict[{0}]不存在技能等级 {1}", skillItem.itemId, levelType));
				}
				else
				{
					Skill skill = this.datebase.Dict[skillItem.itemId][levelType];
					int skill_ID = skill.skill_ID;
					JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(skill_ID)];
					bool flag = false;
					if (this.showLeixing == 6)
					{
						bool flag2 = true;
						foreach (JSONObject jsonobject2 in jsonobject["AttackType"].list)
						{
							if (list.Contains(jsonobject2.I))
							{
								flag2 = false;
							}
						}
						if (flag2)
						{
							flag = true;
						}
					}
					if ((this.showLeixing == 0 || (this.showLeixing != 6 && jsonobject["AttackType"].HasItem(this.showLeixing - 1)) || flag) && (this.ShowType == 0 || jsonobject["Skill_LV"].I == this.ShowType))
					{
						if (num2 >= this.nowIndex * this.SkillNum && num2 < (this.nowIndex + 1) * this.SkillNum)
						{
							this.skill.Add(skill);
							this.skillWin.transform.Find("UIGrid").GetChild(num).GetComponent<SkillCell>().skillID = num;
							num++;
						}
						num2++;
					}
				}
			}
			for (int i = num; i < this.SkillNum; i++)
			{
				this.skillWin.transform.Find("UIGrid").GetChild(num).GetComponent<SkillCell>().skillID = -1;
			}
			if (this.selectpage != null)
			{
				this.selectpage.setMaxPage(num2 / this.SkillNum + 1);
			}
			this.draggingSkill = false;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0003BF2E File Offset: 0x0003A12E
		public void Clear_Draged()
		{
			this.dragedSkill = new Skill();
			this.draggingSkill = false;
			this.Temp.GetComponent<UITexture>().mainTexture = null;
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x0003BF53 File Offset: 0x0003A153
		public void SkillUP(int id)
		{
			if (this.skill[id].skill_level < this.skill[id].Max_level)
			{
				this.skill[id].skill_level++;
			}
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0003BF92 File Offset: 0x0003A192
		public void UseSkill(ref Skill S)
		{
			((Avatar)KBEngineApp.app.player()).spell.spellSkill(S.skill_ID, "");
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x0022F31C File Offset: 0x0022D51C
		public int GetSkillID(int id)
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				if (Tools.instance.getSkillIDByKey(this.skill[i].skill_ID) == id)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x0022F360 File Offset: 0x0022D560
		public void Show_Tooltip(Skill _skill)
		{
			int skill_ID = _skill.skill_ID;
			TooltipItem component = this.Tooltip.GetComponent<TooltipItem>();
			component.Clear();
			JSONObject jsonobject = jsonData.instance.skillJsonData[skill_ID.ToString()];
			component.Label1.text = Tools.getDescByID(_skill.skill_Desc, skill_ID);
			int num = (int)jsonobject["Skill_LV"].n * 2;
			string newValue = jsonData.instance.TootipItemQualityColor[num - 1] + Tools.getStr("pingjie" + (int)jsonobject["Skill_LV"].n) + Tools.getStr("shangzhongxia" + (int)jsonobject["typePinJie"].n);
			component.Label3.text = Tools.getStr("pingjieCell").Replace("{X}", newValue).Replace("[333333]品级：", "");
			component.Label4.text = ((jsonData.instance.TootipItemNameColor[num - 1] + Tools.instance.getSkillName(skill_ID, false)) ?? "");
			string text = "";
			int num2 = 0;
			foreach (JSONObject jsonobject2 in jsonobject["AttackType"].list)
			{
				if (num2 > 0)
				{
					text += "/";
				}
				text += Tools.getStr("xibie" + (int)jsonobject2.n);
				num2++;
			}
			component.Label5.text = text;
			component.icon.mainTexture = _skill.skill_Icon;
			foreach (object obj in component.LingQiGride.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeSelf)
				{
					Object.Destroy(transform.gameObject);
				}
			}
			int num3 = 0;
			foreach (JSONObject jsonobject3 in jsonobject["skill_CastType"].list)
			{
				if (num3 > 0)
				{
					this.CreatGameObjectToParent(component.LingQiGride, component.LingQifengexianImage);
				}
				for (int i = 0; i < (int)jsonobject["skill_Cast"][num3].n; i++)
				{
					this.CreatGameObjectToParent(component.LingQiGride, component.lingqiGridImage).GetComponent<Image>().sprite = component.lingQiGrid[(int)jsonobject3.n];
				}
				num3++;
			}
			int num4 = 0;
			foreach (JSONObject jsonobject4 in jsonobject["skill_SameCastNum"].list)
			{
				if (num3 > 0 || num4 > 0)
				{
					this.CreatGameObjectToParent(component.LingQiGride, component.LingQifengexianImage);
				}
				for (int j = 0; j < (int)jsonobject4.n; j++)
				{
					this.CreatGameObjectToParent(component.LingQiGride, component.lingqiGridImage).GetComponent<Image>().sprite = component.lingQiGrid[component.lingQiGrid.Count - 1];
				}
				num4++;
			}
			component.ShowSkillGride();
			try
			{
				component.Label2.text = "[E0DDB4]暂无说明";
				foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.ItemJsonData)
				{
					if (keyValuePair.Value["type"].I == 3 && jsonobject["Skill_ID"].I == (int)float.Parse(keyValuePair.Value["desc"].str))
					{
						component.Label2.text = "[E0DDB4]" + Tools.instance.Code64ToString(keyValuePair.Value["desc2"].str);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
		{
			GameObject gameObject = Object.Instantiate<Transform>(Temp.transform).gameObject;
			gameObject.transform.SetParent(parent.transform);
			gameObject.SetActive(true);
			gameObject.transform.localScale = Vector3.one;
			return gameObject;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0022F818 File Offset: 0x0022DA18
		public void SaveSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				PlayerPrefs.SetInt("Skill Level" + i, this.skill[i].skill_level);
			}
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0022F864 File Offset: 0x0022DA64
		public void LoadSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				this.skill[i].skill_level = YSSaveGame.GetInt("Skill Level" + i, 0);
			}
		}

		// Token: 0x04005394 RID: 21396
		public List<Skill> skill = new List<Skill>();

		// Token: 0x04005395 RID: 21397
		private SkillDatebase datebase;

		// Token: 0x04005396 RID: 21398
		public GameObject skillWin;

		// Token: 0x04005397 RID: 21399
		public GameObject Tooltip;

		// Token: 0x04005398 RID: 21400
		public bool draggingSkill;

		// Token: 0x04005399 RID: 21401
		public Skill dragedSkill;

		// Token: 0x0400539A RID: 21402
		private bool Show_Skill;

		// Token: 0x0400539B RID: 21403
		public GameObject Temp;

		// Token: 0x0400539C RID: 21404
		public GameObject UIGrid;

		// Token: 0x0400539D RID: 21405
		public selectSkill selectpage;

		// Token: 0x0400539E RID: 21406
		public int SkillNum = 30;

		// Token: 0x0400539F RID: 21407
		public bool showCellName;

		// Token: 0x040053A0 RID: 21408
		public GameObject SkillTemp;

		// Token: 0x040053A1 RID: 21409
		public int ShowType;

		// Token: 0x040053A2 RID: 21410
		public int showLeixing;

		// Token: 0x040053A3 RID: 21411
		public int nowIndex;
	}
}
