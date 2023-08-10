using System;
using KBEngine;
using UnityEngine;

public class showUIJinDan : MonoBehaviour
{
	public TooltipScale tooltips;

	public UILabel iLabel;

	public GameObject Icon;

	public GameObject yuanYinIcon;

	private void Start()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (getJinDanID() != -1)
		{
			Icon.transform.localScale = Vector3.one;
		}
		else
		{
			Icon.transform.localScale = Vector3.zero;
		}
	}

	public int getJinDanID()
	{
		Avatar player = Tools.instance.getPlayer();
		int result = -1;
		foreach (SkillItem hasJieDanSkill in player.hasJieDanSkillList)
		{
			result = hasJieDanSkill.itemId;
		}
		return result;
	}

	public void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			OnHover(isOver: true);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			OnHover(isOver: false);
		}
	}

	private void OnHover(bool isOver)
	{
		int jinDanID = getJinDanID();
		if (isOver && jinDanID != -1)
		{
			if (jinDanID != -1)
			{
				JSONObject jSONObject = jsonData.instance.JieDanBiao[jinDanID.ToString()];
				string text = Tools.Code64(Tools.getStr("shuzi" + (int)jSONObject["JinDanQuality"].n)) + "品" + Tools.Code64(jSONObject["name"].str) + "[-]";
				string text2 = "[c7c479]气血[-][dbffa2]+" + Tools.instance.getPlayer().getJieDanSkillAddHP();
				string text3 = "[c7c479]修炼速度[-][dbffa2]+" + (int)(Math.Ceiling(Tools.instance.getPlayer().getJieDanSkillAddExp() * 100f) - 100.0) + "%";
				string text4 = "";
				if (IsYuanYing())
				{
					text = "[ce49ff]元婴[-]";
					foreach (SkillItem equipStaticSkill in Tools.instance.getPlayer().equipStaticSkillList)
					{
						int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
						if (equipStaticSkill.itemIndex == 6)
						{
							text4 = Tools.instance.getPlayer().getYuanYingStaticDesc(equipStaticSkill, staticSkillKeyByID);
							break;
						}
					}
				}
				string text5 = "";
				int num = 0;
				foreach (JSONObject item in jSONObject["LinGengType"].list)
				{
					text5 = text5 + "\n[c7c479]" + Tools.Code64(Tools.getStr("xibieFight" + (int)item.n)) + "灵根权重[-][dbffa2]+" + (int)jSONObject["LinGengZongShu"][num].n + "[-]";
					num++;
				}
				if (jSONObject["desc"].str != "")
				{
					text5 = text5 + "\n[c7c479]" + Tools.Code64(jSONObject["desc"].str) + "[-]";
				}
				if (IsYuanYing())
				{
					string text6 = text2 + "\n" + text3 + text5 + "\n第二元神：元婴能够单独修炼一门功法，并根据功法属性解锁独有特性.";
					if (text4 != "")
					{
						text6 = text6 + "\n元婴九变：" + Tools.Code64(text4);
					}
					tooltips.uILabel.text = text6;
				}
				else
				{
					tooltips.uILabel.text = text2 + "\n" + text3 + text5;
				}
				iLabel.text = text;
				tooltips.showTooltip = true;
				((Component)tooltips).gameObject.SetActive(true);
			}
			else if (IsYuanYing())
			{
				tooltips.uILabel.text = "尚未凝结金丹";
				tooltips.showTooltip = true;
			}
			else
			{
				tooltips.showTooltip = false;
			}
		}
		else
		{
			tooltips.showTooltip = false;
			((Component)tooltips).gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		int jinDanID = getJinDanID();
		if (IsYuanYing())
		{
			yuanYinIcon.transform.localScale = Vector3.one;
			Icon.transform.localScale = Vector3.zero;
		}
		else if (jinDanID != -1)
		{
			Icon.transform.localScale = Vector3.one;
			yuanYinIcon.transform.localScale = Vector3.zero;
		}
		else
		{
			Icon.transform.localScale = Vector3.zero;
			yuanYinIcon.transform.localScale = Vector3.zero;
		}
	}

	private bool IsYuanYing()
	{
		if (Tools.instance.getPlayer().level >= 10)
		{
			return true;
		}
		return false;
	}
}
