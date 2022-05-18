using System;
using System.Collections.Generic;
using System.Data;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.Fight
{
	// Token: 0x02000E0A RID: 3594
	public class UIFightSkillTip : MonoBehaviour
	{
		// Token: 0x060056D5 RID: 22229 RVA: 0x00241FC8 File Offset: 0x002401C8
		public void SetSkill(GUIPackage.Skill skill)
		{
			_skillJsonData skillJsonData = _skillJsonData.DataDict[skill.skill_ID];
			this.SkillNameText.text = skillJsonData.name.RemoveNumber();
			string text = "<color=#cda14c>说明:</color>" + skillJsonData.descr;
			if (text.Contains("attack"))
			{
				UIFightSkillTip.SetAttackTxt(ref text, skillJsonData.HP);
			}
			text = text.Replace("【", "<color=#42e395>【");
			text = text.Replace("】", "】</color>");
			text = text.STVarReplace();
			this.SkillDescriptionText.text = text;
			string text2 = "";
			int num = 0;
			foreach (int num2 in skillJsonData.AttackType)
			{
				if (num > 0)
				{
					text2 += "-";
				}
				text2 += global::Tools.getStr("xibieFight" + num2);
				num++;
			}
			this.SkillShuXingText.text = text2;
			int num3 = 0;
			foreach (KeyValuePair<int, int> keyValuePair in skill.getSkillCast(global::Tools.instance.getPlayer()))
			{
				int key = keyValuePair.Key;
				int value = keyValuePair.Value;
				this.CostIcons[num3].sprite = this.CostSprites[key];
				this.CostTexts[num3].text = string.Format("x<size=26>{0}</size>", value);
				this.CostIcons[num3].gameObject.SetActive(true);
				this.CostTexts[num3].gameObject.SetActive(true);
				num3++;
			}
			foreach (int num4 in skillJsonData.skill_SameCastNum)
			{
				this.CostIcons[num3].sprite = this.CostSprites[6];
				this.CostTexts[num3].text = string.Format("x<size=26>{0}</size>", num4);
				this.CostIcons[num3].gameObject.SetActive(true);
				this.CostTexts[num3].gameObject.SetActive(true);
				num3++;
			}
			for (int i = num3; i < 5; i++)
			{
				this.CostIcons[i].gameObject.SetActive(false);
				this.CostTexts[i].gameObject.SetActive(false);
			}
			this.WeaponCDText.gameObject.SetActive(false);
			this.CostRoot.SetActive(true);
			if (skillJsonData.Affix2.Count > 0)
			{
				this.CiZhuiText.text = "";
				for (int j = 0; j < skillJsonData.Affix2.Count; j++)
				{
					int num5 = skillJsonData.Affix2[j];
					if (TuJianChunWenBen.DataDict.ContainsKey(num5))
					{
						TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[num5];
						SymbolText ciZhuiText = this.CiZhuiText;
						ciZhuiText.text = string.Concat(new string[]
						{
							ciZhuiText.text,
							"#c42e395【",
							tuJianChunWenBen.name2,
							"】#n",
							tuJianChunWenBen.descr
						});
						if (j != skillJsonData.Affix2.Count - 1)
						{
							SymbolText ciZhuiText2 = this.CiZhuiText;
							ciZhuiText2.text += "\n";
						}
					}
					else
					{
						Debug.LogError(string.Format("没有新词缀{0}", num5));
					}
				}
				this.CiZhuiHengGang.SetActive(true);
				this.CiZhuiText.gameObject.SetActive(true);
				return;
			}
			this.CiZhuiHengGang.SetActive(false);
			this.CiZhuiText.gameObject.SetActive(false);
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x0024240C File Offset: 0x0024060C
		public void SetWeapon(GUIPackage.Skill skill, ITEM_INFO weapon)
		{
			_skillJsonData skillJsonData = _skillJsonData.DataDict[skill.skill_ID];
			this.SkillNameText.text = Inventory2.GetItemName(weapon.Seid, _ItemJsonData.DataDict[weapon.itemId].name);
			this.WeaponCDText.gameObject.SetActive(true);
			this.CostRoot.SetActive(false);
			int oldCD = 0;
			if (SkillSeidJsonData29.DataDict.ContainsKey(skill.skill_ID))
			{
				oldCD = SkillSeidJsonData29.DataDict[skill.skill_ID].value1;
			}
			this.WeaponCDText.text = string.Format("<color=#4493d5>冷却:</color>{0}回合", Inventory2.GetItemCD(weapon.Seid, oldCD));
			string text = "";
			int num = 0;
			JSONObject jsonobject = jsonData.instance.skillJsonData[skill.skill_ID.ToString()];
			foreach (JSONObject jsonobject2 in Inventory2.GetItemAttackType(weapon.Seid, jsonobject["AttackType"]).list)
			{
				if (num > 0)
				{
					text += "-";
				}
				text += global::Tools.getStr("xibieFight" + jsonobject2.I);
				num++;
			}
			this.SkillShuXingText.text = text;
			string text2 = "";
			if (weapon.Seid != null && weapon.Seid.HasField("SeidDesc"))
			{
				text2 = "<color=#50a6ff>主动:</color>" + weapon.Seid["SeidDesc"].Str;
				if (text2.Contains("attack"))
				{
					UIFightSkillTip.SetAttackTxt(ref text2, 1);
				}
			}
			else
			{
				text2 = "<color=#50a6ff>主动:</color>" + jsonData.instance.ItemJsonData[weapon.itemId.ToString()]["desc"].Str;
				if (text2.Contains("attack"))
				{
					UIFightSkillTip.SetAttackTxt(ref text2, 1);
				}
			}
			text2 = text2.Replace("【", "<color=#42e395>【");
			text2 = text2.Replace("】", "】</color>");
			this.SkillDescriptionText.text = text2;
			if (skillJsonData.Affix2.Count > 0)
			{
				this.CiZhuiText.text = "";
				for (int i = 0; i < skillJsonData.Affix2.Count; i++)
				{
					int num2 = skillJsonData.Affix2[i];
					if (TuJianChunWenBen.DataDict.ContainsKey(num2))
					{
						TuJianChunWenBen tuJianChunWenBen = TuJianChunWenBen.DataDict[num2];
						SymbolText ciZhuiText = this.CiZhuiText;
						ciZhuiText.text = string.Concat(new string[]
						{
							ciZhuiText.text,
							"#c42e395【",
							tuJianChunWenBen.name2,
							"】#n",
							tuJianChunWenBen.descr
						});
						if (i != skillJsonData.Affix2.Count - 1)
						{
							SymbolText ciZhuiText2 = this.CiZhuiText;
							ciZhuiText2.text += "\n";
						}
					}
					else
					{
						Debug.LogError(string.Format("没有新词缀{0}", num2));
					}
				}
				this.CiZhuiHengGang.SetActive(true);
				this.CiZhuiText.gameObject.SetActive(true);
				return;
			}
			this.CiZhuiHengGang.SetActive(false);
			this.CiZhuiText.gameObject.SetActive(false);
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x00242790 File Offset: 0x00240990
		private static void SetAttackTxt(ref string desstr, int __attack)
		{
			string text = desstr.Substring(0, desstr.IndexOf("（"));
			string text2 = desstr.Substring(desstr.IndexOf("）"), desstr.Length - desstr.IndexOf("）"));
			string text3 = text2.Substring(1, text2.Length - 1);
			int length = desstr.Length - text3.Length - text.Length - 2;
			string expression = desstr.Substring(desstr.IndexOf("（") + 1, length).Replace("attack", string.Concat(__attack));
			object obj = new DataTable().Compute(expression, "");
			desstr = string.Concat(new object[]
			{
				text,
				"<color=#FF00FF>",
				obj,
				"</color>",
				text3
			});
			if (desstr.IndexOf("attack") > 0)
			{
				UIFightSkillTip.SetAttackTxt(ref desstr, __attack);
			}
		}

		// Token: 0x0400567A RID: 22138
		public Text SkillNameText;

		// Token: 0x0400567B RID: 22139
		public Text SkillDescriptionText;

		// Token: 0x0400567C RID: 22140
		public Text SkillShuXingText;

		// Token: 0x0400567D RID: 22141
		public List<Sprite> CostSprites;

		// Token: 0x0400567E RID: 22142
		public List<Image> CostIcons;

		// Token: 0x0400567F RID: 22143
		public List<Text> CostTexts;

		// Token: 0x04005680 RID: 22144
		public GameObject CostRoot;

		// Token: 0x04005681 RID: 22145
		public GameObject CiZhuiHengGang;

		// Token: 0x04005682 RID: 22146
		public SymbolText CiZhuiText;

		// Token: 0x04005683 RID: 22147
		public Text WeaponCDText;
	}
}
