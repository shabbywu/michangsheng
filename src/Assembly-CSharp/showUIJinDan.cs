using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000470 RID: 1136
public class showUIJinDan : MonoBehaviour
{
	// Token: 0x0600238A RID: 9098 RVA: 0x000F328B File Offset: 0x000F148B
	private void Start()
	{
		if (this.getJinDanID() != -1)
		{
			this.Icon.transform.localScale = Vector3.one;
			return;
		}
		this.Icon.transform.localScale = Vector3.zero;
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x000F32C4 File Offset: 0x000F14C4
	public int getJinDanID()
	{
		Avatar player = Tools.instance.getPlayer();
		int result = -1;
		foreach (SkillItem skillItem in player.hasJieDanSkillList)
		{
			result = skillItem.itemId;
		}
		return result;
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x000F3324 File Offset: 0x000F1524
	public void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.OnHover(true);
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.OnHover(false);
		}
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x000F3348 File Offset: 0x000F1548
	private void OnHover(bool isOver)
	{
		int jinDanID = this.getJinDanID();
		if (!isOver || jinDanID == -1)
		{
			this.tooltips.showTooltip = false;
			this.tooltips.gameObject.SetActive(false);
			return;
		}
		if (jinDanID != -1)
		{
			JSONObject jsonobject = jsonData.instance.JieDanBiao[jinDanID.ToString()];
			string text = Tools.Code64(Tools.getStr("shuzi" + (int)jsonobject["JinDanQuality"].n)) + "品" + Tools.Code64(jsonobject["name"].str) + "[-]";
			string text2 = "[c7c479]气血[-][dbffa2]+" + Tools.instance.getPlayer().getJieDanSkillAddHP();
			string text3 = "[c7c479]修炼速度[-][dbffa2]+" + (int)(Math.Ceiling((double)(Tools.instance.getPlayer().getJieDanSkillAddExp() * 100f)) - 100.0) + "%";
			string text4 = "";
			if (this.IsYuanYing())
			{
				text = "[ce49ff]元婴[-]";
				foreach (SkillItem skillItem in Tools.instance.getPlayer().equipStaticSkillList)
				{
					int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
					if (skillItem.itemIndex == 6)
					{
						text4 = Tools.instance.getPlayer().getYuanYingStaticDesc(skillItem, staticSkillKeyByID);
						break;
					}
				}
			}
			string text5 = "";
			int num = 0;
			foreach (JSONObject jsonobject2 in jsonobject["LinGengType"].list)
			{
				text5 = string.Concat(new object[]
				{
					text5,
					"\n[c7c479]",
					Tools.Code64(Tools.getStr("xibieFight" + (int)jsonobject2.n)),
					"灵根权重[-][dbffa2]+",
					(int)jsonobject["LinGengZongShu"][num].n,
					"[-]"
				});
				num++;
			}
			if (jsonobject["desc"].str != "")
			{
				text5 = text5 + "\n[c7c479]" + Tools.Code64(jsonobject["desc"].str) + "[-]";
			}
			if (this.IsYuanYing())
			{
				string text6 = string.Concat(new string[]
				{
					text2,
					"\n",
					text3,
					text5,
					"\n第二元神：元婴能够单独修炼一门功法，并根据功法属性解锁独有特性."
				});
				if (text4 != "")
				{
					text6 = text6 + "\n元婴九变：" + Tools.Code64(text4);
				}
				this.tooltips.uILabel.text = text6;
			}
			else
			{
				this.tooltips.uILabel.text = text2 + "\n" + text3 + text5;
			}
			this.iLabel.text = text;
			this.tooltips.showTooltip = true;
			this.tooltips.gameObject.SetActive(true);
			return;
		}
		if (this.IsYuanYing())
		{
			this.tooltips.uILabel.text = "尚未凝结金丹";
			this.tooltips.showTooltip = true;
			return;
		}
		this.tooltips.showTooltip = false;
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x000F36D8 File Offset: 0x000F18D8
	private void Update()
	{
		int jinDanID = this.getJinDanID();
		if (this.IsYuanYing())
		{
			this.yuanYinIcon.transform.localScale = Vector3.one;
			this.Icon.transform.localScale = Vector3.zero;
			return;
		}
		if (jinDanID != -1)
		{
			this.Icon.transform.localScale = Vector3.one;
			this.yuanYinIcon.transform.localScale = Vector3.zero;
			return;
		}
		this.Icon.transform.localScale = Vector3.zero;
		this.yuanYinIcon.transform.localScale = Vector3.zero;
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x000F3778 File Offset: 0x000F1978
	private bool IsYuanYing()
	{
		return Tools.instance.getPlayer().level >= 10;
	}

	// Token: 0x04001C7B RID: 7291
	public TooltipScale tooltips;

	// Token: 0x04001C7C RID: 7292
	public UILabel iLabel;

	// Token: 0x04001C7D RID: 7293
	public GameObject Icon;

	// Token: 0x04001C7E RID: 7294
	public GameObject yuanYinIcon;
}
