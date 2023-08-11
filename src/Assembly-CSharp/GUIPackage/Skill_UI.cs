using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

namespace GUIPackage;

public class Skill_UI : MonoBehaviour
{
	public List<Skill> skill = new List<Skill>();

	private SkillDatebase datebase;

	public GameObject skillWin;

	public GameObject Tooltip;

	public bool draggingSkill;

	public Skill dragedSkill;

	private bool Show_Skill;

	public GameObject Temp;

	public GameObject UIGrid;

	public selectSkill selectpage;

	public int SkillNum = 30;

	public bool showCellName;

	public GameObject SkillTemp;

	public int ShowType;

	public int showLeixing;

	public int nowIndex;

	public bool showTooltip
	{
		get
		{
			bool result = false;
			if ((Object)(object)Tooltip.GetComponent<TooltipScale>() != (Object)null)
			{
				result = Tooltip.GetComponent<TooltipScale>().showTooltip;
			}
			else if (Object.op_Implicit((Object)(object)Tooltip.GetComponent<TooltipItem>()))
			{
				result = Tooltip.GetComponent<TooltipItem>().showTooltip;
			}
			return result;
		}
		set
		{
			if ((Object)(object)Tooltip.GetComponent<TooltipScale>() != (Object)null)
			{
				Tooltip.GetComponent<TooltipScale>().showTooltip = value;
			}
			else if (Object.op_Implicit((Object)(object)Tooltip.GetComponent<TooltipItem>()))
			{
				Tooltip.GetComponent<TooltipItem>().showTooltip = value;
			}
		}
	}

	private void Awake()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		datebase = ((Component)jsonData.instance).gameObject.GetComponent<SkillDatebase>();
		if ((Object)(object)SkillTemp == (Object)null)
		{
			ref GameObject skillTemp = ref SkillTemp;
			Object obj = Resources.Load("skill");
			skillTemp = (GameObject)(object)((obj is GameObject) ? obj : null);
		}
		for (int i = 0; i < SkillNum; i++)
		{
			GameObject obj2 = Object.Instantiate<GameObject>(SkillTemp);
			obj2.SetActive(true);
			obj2.GetComponent<SkillCell>().skillID = -1;
			obj2.GetComponent<SkillCell>().showName = showCellName;
			obj2.transform.parent = ((Component)skillWin.transform.Find("UIGrid")).transform;
			obj2.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		((Component)this).GetComponentInChildren<UIGrid>().repositionNow = true;
		initSkill_UI();
	}

	private void Update()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (draggingSkill)
		{
			if (Input.GetMouseButtonUp(0))
			{
				Clear_Draged();
			}
			Temp.transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
			Temp.GetComponent<UITexture>().mainTexture = (Texture)(object)dragedSkill.skill_Icon;
			Singleton.skillUI.showTooltip = false;
		}
		skillCD();
	}

	private void skillCD()
	{
		for (int i = 0; i < skill.Count; i++)
		{
			if (skill[i].CurCD != 0f)
			{
				skill[i].CurCD -= Time.deltaTime;
				if (skill[i].CurCD <= 0f)
				{
					skill[i].CurCD = 0f;
				}
			}
		}
	}

	private void Show()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Show_Skill = !Show_Skill;
		if (!Show_Skill)
		{
			Singleton.inventory.showTooltip = false;
		}
		if (Show_Skill)
		{
			((Component)this).transform.Find("Win").position = ((Component)this).transform.position;
		}
		skillWin.SetActive(Show_Skill);
		Singleton.UI.UI_Top(skillWin.transform.parent);
	}

	public void ShowSkillType()
	{
	}

	private void initSkill_UI()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (avatar == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		List<int> list = new List<int> { 0, 1, 2, 3, 4 };
		int levelType = avatar.getLevelType();
		foreach (SkillItem hasSkill in avatar.hasSkillList)
		{
			if (!datebase.Dict.ContainsKey(hasSkill.itemId))
			{
				Debug.LogError((object)$"初始化技能UI出错，技能数据库datebase.Dict不存在技能ID {hasSkill.itemId}");
				continue;
			}
			if (!datebase.Dict[hasSkill.itemId].ContainsKey(levelType))
			{
				Debug.LogError((object)$"初始化技能UI出错，技能数据库datebase.Dict[{hasSkill.itemId}]不存在技能等级 {levelType}");
				continue;
			}
			Skill skill = datebase.Dict[hasSkill.itemId][levelType];
			int skill_ID = skill.skill_ID;
			JSONObject jSONObject = jsonData.instance.skillJsonData[string.Concat(skill_ID)];
			bool flag = false;
			if (showLeixing == 6)
			{
				bool flag2 = true;
				foreach (JSONObject item in jSONObject["AttackType"].list)
				{
					if (list.Contains(item.I))
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					flag = true;
				}
			}
			if ((showLeixing == 0 || (showLeixing != 6 && jSONObject["AttackType"].HasItem(showLeixing - 1)) || flag) && (ShowType == 0 || jSONObject["Skill_LV"].I == ShowType))
			{
				if (num2 >= nowIndex * SkillNum && num2 < (nowIndex + 1) * SkillNum)
				{
					this.skill.Add(skill);
					((Component)skillWin.transform.Find("UIGrid").GetChild(num)).GetComponent<SkillCell>().skillID = num;
					num++;
				}
				num2++;
			}
		}
		for (int i = num; i < SkillNum; i++)
		{
			((Component)skillWin.transform.Find("UIGrid").GetChild(num)).GetComponent<SkillCell>().skillID = -1;
		}
		if ((Object)(object)selectpage != (Object)null)
		{
			selectpage.setMaxPage(num2 / SkillNum + 1);
		}
		draggingSkill = false;
	}

	public void Clear_Draged()
	{
		dragedSkill = new Skill();
		draggingSkill = false;
		Temp.GetComponent<UITexture>().mainTexture = null;
	}

	public void SkillUP(int id)
	{
		if (skill[id].skill_level < skill[id].Max_level)
		{
			skill[id].skill_level++;
		}
	}

	public void UseSkill(ref Skill S)
	{
		((Avatar)KBEngineApp.app.player()).spell.spellSkill(S.skill_ID);
	}

	public int GetSkillID(int id)
	{
		for (int i = 0; i < skill.Count; i++)
		{
			if (Tools.instance.getSkillIDByKey(skill[i].skill_ID) == id)
			{
				return i;
			}
		}
		return -1;
	}

	public void Show_Tooltip(Skill _skill)
	{
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		int skill_ID = _skill.skill_ID;
		TooltipItem component = Tooltip.GetComponent<TooltipItem>();
		component.Clear();
		JSONObject jSONObject = jsonData.instance.skillJsonData[skill_ID.ToString()];
		component.Label1.text = Tools.getDescByID(_skill.skill_Desc, skill_ID);
		string text = "";
		int num = (int)jSONObject["Skill_LV"].n * 2;
		text = jsonData.instance.TootipItemQualityColor[num - 1] + Tools.getStr("pingjie" + (int)jSONObject["Skill_LV"].n) + Tools.getStr("shangzhongxia" + (int)jSONObject["typePinJie"].n);
		component.Label3.text = Tools.getStr("pingjieCell").Replace("{X}", text).Replace("[333333]品级：", "");
		component.Label4.text = (jsonData.instance.TootipItemNameColor[num - 1] + Tools.instance.getSkillName(skill_ID)) ?? "";
		string text2 = "";
		int num2 = 0;
		foreach (JSONObject item in jSONObject["AttackType"].list)
		{
			if (num2 > 0)
			{
				text2 += "/";
			}
			text2 += Tools.getStr("xibie" + (int)item.n);
			num2++;
		}
		component.Label5.text = text2;
		component.icon.mainTexture = (Texture)(object)_skill.skill_Icon;
		foreach (Transform item2 in component.LingQiGride.transform)
		{
			Transform val = item2;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		int num3 = 0;
		foreach (JSONObject item3 in jSONObject["skill_CastType"].list)
		{
			if (num3 > 0)
			{
				CreatGameObjectToParent(component.LingQiGride, component.LingQifengexianImage);
			}
			for (int i = 0; i < (int)jSONObject["skill_Cast"][num3].n; i++)
			{
				CreatGameObjectToParent(component.LingQiGride, component.lingqiGridImage).GetComponent<Image>().sprite = component.lingQiGrid[(int)item3.n];
			}
			num3++;
		}
		int num4 = 0;
		foreach (JSONObject item4 in jSONObject["skill_SameCastNum"].list)
		{
			if (num3 > 0 || num4 > 0)
			{
				CreatGameObjectToParent(component.LingQiGride, component.LingQifengexianImage);
			}
			for (int j = 0; j < (int)item4.n; j++)
			{
				CreatGameObjectToParent(component.LingQiGride, component.lingqiGridImage).GetComponent<Image>().sprite = component.lingQiGrid[component.lingQiGrid.Count - 1];
			}
			num4++;
		}
		component.ShowSkillGride();
		try
		{
			component.Label2.text = "[E0DDB4]暂无说明";
			foreach (KeyValuePair<string, JSONObject> itemJsonDatum in jsonData.instance.ItemJsonData)
			{
				if (itemJsonDatum.Value["type"].I == 3 && jSONObject["Skill_ID"].I == (int)float.Parse(itemJsonDatum.Value["desc"].str))
				{
					component.Label2.text = "[E0DDB4]" + Tools.instance.Code64ToString(itemJsonDatum.Value["desc2"].str);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = ((Component)Object.Instantiate<Transform>(Temp.transform)).gameObject;
		gameObject.transform.SetParent(parent.transform);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}

	public void SaveSkill()
	{
		for (int i = 0; i < skill.Count; i++)
		{
			PlayerPrefs.SetInt("Skill Level" + i, skill[i].skill_level);
		}
	}

	public void LoadSkill()
	{
		for (int i = 0; i < skill.Count; i++)
		{
			skill[i].skill_level = YSSaveGame.GetInt("Skill Level" + i);
		}
	}
}
