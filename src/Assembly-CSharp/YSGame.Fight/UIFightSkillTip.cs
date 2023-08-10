using System.Collections.Generic;
using System.Data;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.Fight;

public class UIFightSkillTip : MonoBehaviour
{
	public Text SkillNameText;

	public Text SkillDescriptionText;

	public Text SkillShuXingText;

	public List<Sprite> CostSprites;

	public List<Image> CostIcons;

	public List<Text> CostTexts;

	public GameObject CostRoot;

	public GameObject CiZhuiHengGang;

	public SymbolText CiZhuiText;

	public Text WeaponCDText;

	public void SetSkill(GUIPackage.Skill skill)
	{
		_skillJsonData skillJsonData = _skillJsonData.DataDict[skill.skill_ID];
		SkillNameText.text = skillJsonData.name.RemoveNumber();
		string desstr = "<color=#cda14c>说明:</color>" + skillJsonData.descr;
		if (desstr.Contains("attack"))
		{
			SetAttackTxt(ref desstr, skillJsonData.HP);
		}
		desstr = desstr.Replace("【", "<color=#42e395>【");
		desstr = desstr.Replace("】", "】</color>");
		desstr = desstr.STVarReplace();
		SkillDescriptionText.text = desstr;
		string text = "";
		int num = 0;
		foreach (int item in skillJsonData.AttackType)
		{
			if (num > 0)
			{
				text += "-";
			}
			text += Tools.getStr("xibieFight" + item);
			num++;
		}
		SkillShuXingText.text = text;
		int num2 = 0;
		foreach (KeyValuePair<int, int> item2 in skill.getSkillCast(Tools.instance.getPlayer()))
		{
			int key = item2.Key;
			int value = item2.Value;
			CostIcons[num2].sprite = CostSprites[key];
			CostTexts[num2].text = $"x<size=26>{value}</size>";
			((Component)CostIcons[num2]).gameObject.SetActive(true);
			((Component)CostTexts[num2]).gameObject.SetActive(true);
			num2++;
		}
		foreach (int item3 in skillJsonData.skill_SameCastNum)
		{
			CostIcons[num2].sprite = CostSprites[6];
			CostTexts[num2].text = $"x<size=26>{item3}</size>";
			((Component)CostIcons[num2]).gameObject.SetActive(true);
			((Component)CostTexts[num2]).gameObject.SetActive(true);
			num2++;
		}
		for (int i = num2; i < 5; i++)
		{
			((Component)CostIcons[i]).gameObject.SetActive(false);
			((Component)CostTexts[i]).gameObject.SetActive(false);
		}
		((Component)WeaponCDText).gameObject.SetActive(false);
		CostRoot.SetActive(true);
		if (skillJsonData.Affix2.Count > 0)
		{
			((Text)CiZhuiText).text = "";
			for (int j = 0; j < skillJsonData.Affix2.Count; j++)
			{
				int num3 = skillJsonData.Affix2[j];
				if (TuJianChunWenBen.DataDict.ContainsKey(num3))
				{
					TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[num3];
					SymbolText ciZhuiText = CiZhuiText;
					((Text)ciZhuiText).text = ((Text)ciZhuiText).text + "#c42e395【" + tuJianChunWenBen.name2 + "】#n" + tuJianChunWenBen.descr;
					if (j != skillJsonData.Affix2.Count - 1)
					{
						SymbolText ciZhuiText2 = CiZhuiText;
						((Text)ciZhuiText2).text = ((Text)ciZhuiText2).text + "\n";
					}
				}
				else
				{
					Debug.LogError((object)$"没有新词缀{num3}");
				}
			}
			CiZhuiHengGang.SetActive(true);
			((Component)CiZhuiText).gameObject.SetActive(true);
		}
		else
		{
			CiZhuiHengGang.SetActive(false);
			((Component)CiZhuiText).gameObject.SetActive(false);
		}
	}

	public void SetWeapon(GUIPackage.Skill skill, ITEM_INFO weapon)
	{
		_skillJsonData skillJsonData = _skillJsonData.DataDict[skill.skill_ID];
		SkillNameText.text = Inventory2.GetItemName(weapon.Seid, _ItemJsonData.DataDict[weapon.itemId].name);
		((Component)WeaponCDText).gameObject.SetActive(true);
		CostRoot.SetActive(false);
		int oldCD = 0;
		if (SkillSeidJsonData29.DataDict.ContainsKey(skill.skill_ID))
		{
			oldCD = SkillSeidJsonData29.DataDict[skill.skill_ID].value1;
		}
		WeaponCDText.text = $"<color=#4493d5>冷却:</color>{Inventory2.GetItemCD(weapon.Seid, oldCD)}回合";
		string text = "";
		int num = 0;
		JSONObject jSONObject = jsonData.instance.skillJsonData[skill.skill_ID.ToString()];
		foreach (JSONObject item in Inventory2.GetItemAttackType(weapon.Seid, jSONObject["AttackType"]).list)
		{
			if (num > 0)
			{
				text += "-";
			}
			text += Tools.getStr("xibieFight" + item.I);
			num++;
		}
		SkillShuXingText.text = text;
		string text2 = "";
		if (weapon.Seid != null && weapon.Seid.HasField("SeidDesc"))
		{
			text2 = "<color=#50a6ff>主动:</color>" + weapon.Seid["SeidDesc"].Str;
			if (text2.Contains("attack"))
			{
				SetAttackTxt(ref text2, 1);
			}
		}
		else
		{
			text2 = "<color=#50a6ff>主动:</color>" + jsonData.instance.ItemJsonData[weapon.itemId.ToString()]["desc"].Str;
			if (text2.Contains("attack"))
			{
				SetAttackTxt(ref text2, 1);
			}
		}
		text2 = text2.Replace("【", "<color=#42e395>【");
		text2 = text2.Replace("】", "】</color>");
		SkillDescriptionText.text = text2;
		if (skillJsonData.Affix2.Count > 0)
		{
			((Text)CiZhuiText).text = "";
			for (int i = 0; i < skillJsonData.Affix2.Count; i++)
			{
				int num2 = skillJsonData.Affix2[i];
				if (TuJianChunWenBen.DataDict.ContainsKey(num2))
				{
					TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[num2];
					SymbolText ciZhuiText = CiZhuiText;
					((Text)ciZhuiText).text = ((Text)ciZhuiText).text + "#c42e395【" + tuJianChunWenBen.name2 + "】#n" + tuJianChunWenBen.descr;
					if (i != skillJsonData.Affix2.Count - 1)
					{
						SymbolText ciZhuiText2 = CiZhuiText;
						((Text)ciZhuiText2).text = ((Text)ciZhuiText2).text + "\n";
					}
				}
				else
				{
					Debug.LogError((object)$"没有新词缀{num2}");
				}
			}
			CiZhuiHengGang.SetActive(true);
			((Component)CiZhuiText).gameObject.SetActive(true);
		}
		else
		{
			CiZhuiHengGang.SetActive(false);
			((Component)CiZhuiText).gameObject.SetActive(false);
		}
	}

	private static void SetAttackTxt(ref string desstr, int __attack)
	{
		string text = desstr.Substring(0, desstr.IndexOf("（"));
		string text2 = desstr.Substring(desstr.IndexOf("）"), desstr.Length - desstr.IndexOf("）"));
		string text3 = text2.Substring(1, text2.Length - 1);
		int length = desstr.Length - text3.Length - text.Length - 2;
		string expression = desstr.Substring(desstr.IndexOf("（") + 1, length).Replace("attack", string.Concat(__attack));
		object obj = new DataTable().Compute(expression, "");
		desstr = string.Concat(text, "<color=#FF00FF>", obj, "</color>", text3);
		if (desstr.IndexOf("attack") > 0)
		{
			SetAttackTxt(ref desstr, __attack);
		}
	}
}
