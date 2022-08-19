using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

namespace GUIPackage
{
	// Token: 0x02000A68 RID: 2664
	public class Skill_UI : MonoBehaviour
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x001FCC24 File Offset: 0x001FAE24
		// (set) Token: 0x06004ACE RID: 19150 RVA: 0x001FCBCC File Offset: 0x001FADCC
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

		// Token: 0x06004AD0 RID: 19152 RVA: 0x001FCC80 File Offset: 0x001FAE80
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

		// Token: 0x06004AD1 RID: 19153 RVA: 0x001FCD5C File Offset: 0x001FAF5C
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

		// Token: 0x06004AD2 RID: 19154 RVA: 0x001FCDCC File Offset: 0x001FAFCC
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

		// Token: 0x06004AD3 RID: 19155 RVA: 0x001FCE54 File Offset: 0x001FB054
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

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00004095 File Offset: 0x00002295
		public void ShowSkillType()
		{
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x001FCED8 File Offset: 0x001FB0D8
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

		// Token: 0x06004AD6 RID: 19158 RVA: 0x001FD1DC File Offset: 0x001FB3DC
		public void Clear_Draged()
		{
			this.dragedSkill = new Skill();
			this.draggingSkill = false;
			this.Temp.GetComponent<UITexture>().mainTexture = null;
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x001FD201 File Offset: 0x001FB401
		public void SkillUP(int id)
		{
			if (this.skill[id].skill_level < this.skill[id].Max_level)
			{
				this.skill[id].skill_level++;
			}
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x001FD240 File Offset: 0x001FB440
		public void UseSkill(ref Skill S)
		{
			((Avatar)KBEngineApp.app.player()).spell.spellSkill(S.skill_ID, "");
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x001FD268 File Offset: 0x001FB468
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

		// Token: 0x06004ADA RID: 19162 RVA: 0x001FD2AC File Offset: 0x001FB4AC
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

		// Token: 0x06004ADB RID: 19163 RVA: 0x000EBA04 File Offset: 0x000E9C04
		public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
		{
			GameObject gameObject = Object.Instantiate<Transform>(Temp.transform).gameObject;
			gameObject.transform.SetParent(parent.transform);
			gameObject.SetActive(true);
			gameObject.transform.localScale = Vector3.one;
			return gameObject;
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x001FD764 File Offset: 0x001FB964
		public void SaveSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				PlayerPrefs.SetInt("Skill Level" + i, this.skill[i].skill_level);
			}
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001FD7B0 File Offset: 0x001FB9B0
		public void LoadSkill()
		{
			for (int i = 0; i < this.skill.Count; i++)
			{
				this.skill[i].skill_level = YSSaveGame.GetInt("Skill Level" + i, 0);
			}
		}

		// Token: 0x040049F7 RID: 18935
		public List<Skill> skill = new List<Skill>();

		// Token: 0x040049F8 RID: 18936
		private SkillDatebase datebase;

		// Token: 0x040049F9 RID: 18937
		public GameObject skillWin;

		// Token: 0x040049FA RID: 18938
		public GameObject Tooltip;

		// Token: 0x040049FB RID: 18939
		public bool draggingSkill;

		// Token: 0x040049FC RID: 18940
		public Skill dragedSkill;

		// Token: 0x040049FD RID: 18941
		private bool Show_Skill;

		// Token: 0x040049FE RID: 18942
		public GameObject Temp;

		// Token: 0x040049FF RID: 18943
		public GameObject UIGrid;

		// Token: 0x04004A00 RID: 18944
		public selectSkill selectpage;

		// Token: 0x04004A01 RID: 18945
		public int SkillNum = 30;

		// Token: 0x04004A02 RID: 18946
		public bool showCellName;

		// Token: 0x04004A03 RID: 18947
		public GameObject SkillTemp;

		// Token: 0x04004A04 RID: 18948
		public int ShowType;

		// Token: 0x04004A05 RID: 18949
		public int showLeixing;

		// Token: 0x04004A06 RID: 18950
		public int nowIndex;
	}
}
