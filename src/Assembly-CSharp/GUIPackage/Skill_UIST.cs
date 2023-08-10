using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame;

namespace GUIPackage;

public class Skill_UIST : MonoBehaviour
{
	public List<Skill> skill = new List<Skill>();

	private SkillStaticDatebase datebase;

	public GameObject skillWin;

	public GameObject Tooltip;

	public bool draggingSkill;

	public Skill dragedSkill;

	private bool Show_Skill;

	public GameObject Temp;

	public GameObject UIGrid;

	public GameObject UPSkillUI;

	public GameObject SkillTemp;

	public int nowIndex;

	public int ShowType;

	public int OnePageNum;

	public selectStaticSkill selectpage;

	public bool showCellName;

	public bool showDengjie;

	public int showLeixing;

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
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		datebase = ((Component)jsonData.instance).gameObject.GetComponent<SkillStaticDatebase>();
		if ((Object)(object)SkillTemp == (Object)null)
		{
			ref GameObject skillTemp = ref SkillTemp;
			Object obj = Resources.Load("StaticSkill");
			skillTemp = (GameObject)(object)((obj is GameObject) ? obj : null);
		}
		if (OnePageNum == 0)
		{
			OnePageNum = 30;
		}
		for (int i = 0; i < OnePageNum; i++)
		{
			GameObject val = Object.Instantiate<GameObject>(SkillTemp);
			val.SetActive(true);
			if ((Object)(object)val.GetComponent<SkillStaticCell>() != (Object)null)
			{
				val.GetComponent<SkillStaticCell>().skillID = -1;
				val.GetComponent<SkillStaticCell>().skill_UIST = this;
				val.GetComponent<SkillStaticCell>().showName = showCellName;
				val.GetComponent<SkillStaticCell>().showDengji = showDengjie;
			}
			else if ((Object)(object)val.GetComponent<StaticTuPoCell>() != (Object)null)
			{
				val.GetComponent<StaticTuPoCell>().skillID = -1;
				val.GetComponent<SkillStaticCell>().skill_UIST = this;
			}
			val.transform.parent = ((Component)skillWin.transform.Find("UIGrid")).transform;
			val.transform.localScale = new Vector3(1f, 1f, 1f);
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
			Singleton.skillUI2.showTooltip = false;
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
		foreach (SkillItem hasStaticSkill in avatar.hasStaticSkillList)
		{
			if (!datebase.Dict.ContainsKey(hasStaticSkill.itemId))
			{
				Debug.LogError((object)$"初始化技能UI出错，技能数据库datebase.Dict不存在技能ID {hasStaticSkill.itemId}");
				continue;
			}
			if (!datebase.Dict[hasStaticSkill.itemId].ContainsKey(hasStaticSkill.level))
			{
				Debug.LogError((object)$"初始化技能UI出错，技能数据库datebase.Dict[{hasStaticSkill.itemId}]不存在技能等级 {hasStaticSkill.level}");
				continue;
			}
			Skill skill = datebase.Dict[hasStaticSkill.itemId][hasStaticSkill.level];
			int skill_ID = skill.skill_ID;
			JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[string.Concat(skill_ID)];
			bool flag = false;
			if (showLeixing == 6 && !list.Contains(jSONObject["AttackType"].I))
			{
				flag = true;
			}
			if (!(showLeixing == 0 || (showLeixing != 6 && jSONObject["AttackType"].I == showLeixing - 1) || flag) || (ShowType != 0 && jSONObject["Skill_LV"].I != ShowType))
			{
				continue;
			}
			if (num2 >= nowIndex * OnePageNum && num2 < (nowIndex + 1) * OnePageNum)
			{
				this.skill.Add(skill);
				GameObject gameObject = ((Component)skillWin.transform.Find("UIGrid").GetChild(num)).gameObject;
				if ((Object)(object)gameObject.GetComponent<SkillStaticCell>() != (Object)null)
				{
					gameObject.GetComponent<SkillStaticCell>().skillID = num;
				}
				else if ((Object)(object)gameObject.GetComponent<StaticTuPoCell>() != (Object)null)
				{
					gameObject.GetComponent<StaticTuPoCell>().skillID = num;
				}
				num++;
			}
			num2++;
		}
		for (int i = num; i < OnePageNum; i++)
		{
			GameObject gameObject2 = ((Component)skillWin.transform.Find("UIGrid").GetChild(num)).gameObject;
			if ((Object)(object)gameObject2.GetComponent<SkillStaticCell>() != (Object)null)
			{
				gameObject2.GetComponent<SkillStaticCell>().skillID = -1;
			}
			else if ((Object)(object)gameObject2.GetComponent<StaticTuPoCell>() != (Object)null)
			{
				gameObject2.GetComponent<StaticTuPoCell>().skillID = -1;
			}
		}
		if ((Object)(object)selectpage != (Object)null)
		{
			selectpage.maxPage = num2 / OnePageNum + 1;
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		UPSkillUI.SetActive(true);
		UPSkillUI.transform.localPosition = Vector3.zero;
		UPSkillUI.transform.localScale = Vector3.one;
		UPSkillUI.GetComponent<UI_UPSkill>().initSkill(Tools.instance.getStaticSkillIDByKey(skill[id].skill_ID));
	}

	public void UseSkill(ref Skill S)
	{
		((Avatar)KBEngineApp.app.player()).spell.spellSkill(S.skill_ID);
	}

	public int GetSkillID(int id)
	{
		for (int i = 0; i < skill.Count; i++)
		{
			if (Tools.instance.getStaticSkillIDByKey(skill[i].skill_ID) == id)
			{
				return i;
			}
		}
		return -1;
	}

	public void Show_Tooltip(Skill _skill, int STSKillType = 0)
	{
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		int skill_ID = _skill.skill_ID;
		TooltipItem component = Tooltip.GetComponent<TooltipItem>();
		component.Clear();
		JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[skill_ID.ToString()];
		component.Label1.text = "[E0DDB4]" + _skill.skill_Desc;
		string text = "";
		int num = (int)jSONObject["Skill_LV"].n * 2;
		text = jsonData.instance.TootipItemQualityColor[num - 1] + Tools.getStr("pingjie" + (int)jSONObject["Skill_LV"].n) + Tools.getStr("shangzhongxia" + (int)jSONObject["typePinJie"].n);
		component.Label3.text = Tools.getStr("pingjieCell").Replace("{X}", text).Replace("[333333]品级：", "");
		component.Label4.text = (jsonData.instance.TootipItemNameColor[num - 1] + Tools.instance.getStaticSkillName(skill_ID)) ?? "";
		string str = Tools.getStr("gongfaleibie" + (int)jSONObject["AttackType"].n);
		component.Label5.text = str;
		component.icon.mainTexture = (Texture)(object)_skill.skill_Icon;
		if ((Object)(object)component.TooltipHelp != (Object)null)
		{
			foreach (Transform item in component.TooltipHelp.transform.parent)
			{
				Transform val = item;
				if (((Component)val).gameObject.activeSelf)
				{
					Object.Destroy((Object)(object)((Component)val).gameObject);
				}
			}
		}
		if (STSKillType == 1 && (Object)(object)component.TooltipHelp != (Object)null)
		{
			JSONObject gongFaBookItem = GUIPackage.item.getGongFaBookItem(jSONObject["Skill_ID"].I);
			List<int> wudaoTypeList = new List<int>();
			GUIPackage.item.GetWuDaoType(wudaoLvList: new List<int>(), itemID: gongFaBookItem["id"].I, wudaoTypeList: wudaoTypeList);
			string desc = "[d3b068]悟道提升：[-]" + GUIPackage.item.StudyTiSheng(wudaoTypeList, "突破后能够提升");
			component.ShowSkillTime(desc);
		}
		component.setCenterTextTitle("【进度】", "【修炼速度】");
		component.Label7.text = "第" + Tools.getStr("cengshu" + (int)jSONObject["Skill_Lv"].n);
		component.Label8.text = (int)jSONObject["Skill_Speed"].n + "/月";
		try
		{
			bool flag = false;
			foreach (KeyValuePair<string, JSONObject> itemJsonDatum in jsonData.instance.ItemJsonData)
			{
				if ((int)itemJsonDatum.Value["type"].n == 4 && jSONObject["Skill_ID"].I == (int)float.Parse(itemJsonDatum.Value["desc"].str))
				{
					component.Label2.text = "[E0DDB4]" + Tools.instance.Code64ToString(itemJsonDatum.Value["desc2"].str);
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
