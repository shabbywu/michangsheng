using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class headUIMag : MonoBehaviour
{
	// Token: 0x06001352 RID: 4946 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x000B309C File Offset: 0x000B129C
	public void showHeadUI()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Transform transform = base.transform.Find("Text");
		string str = "0/年";
		foreach (SkillItem skillItem in avatar.equipStaticSkillList)
		{
			if (skillItem.itemIndex == 0)
			{
				Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
				str = ((int)avatar.getTimeExpSpeed()).ToString();
				break;
			}
		}
		transform.Find("name").GetComponent<UILabel>().text = (avatar.name ?? "");
		transform.Find("age").GetComponent<UILabel>().text = string.Concat(avatar.age);
		transform.Find("shouYuan").GetComponent<UILabel>().text = string.Concat(avatar.shouYuan);
		transform.Find("XiuLian").GetComponent<UILabel>().text = str + "/月";
		transform.Find("zhiwei").GetComponent<UILabel>().text = PlayerEx.GetMenPaiChengHao();
		transform.Find("fenglu").GetComponent<UILabel>().text = avatar.chenghaomag.GetOneYearAddMoney() + "灵石/年";
		transform.Find("shengwang").GetComponent<UILabel>().text = PlayerEx.GetMenPaiShengWang().ToString();
		string text = Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[avatar.level.ToString()]["Name"].str) ?? "";
		base.transform.Find("LV/Label").GetComponent<UILabel>().text = text.Insert(2, "   ");
		UI2DSprite component = base.transform.Find("LV/2D Sprite").GetComponent<UI2DSprite>();
		component.sprite2D = Tools.instance.getLevelSprite((int)avatar.level, component.sprite2D.rect);
		base.transform.Find("MenPai/Label").GetComponent<UILabel>().text = Tools.getStr("menpai" + avatar.menPai);
		int num = 0;
		foreach (int num2 in avatar.GetLingGeng)
		{
			num += num2;
		}
		int num3 = avatar.GetLingGeng.Max();
		for (int i = 0; i < 5; i++)
		{
			base.transform.Find("LingGen/icon" + i).GetComponent<UI2DSprite>().alpha = (float)avatar.GetLingGeng[i] / (float)num3;
			base.transform.Find("LingGen/zi" + i).GetComponent<UILabel>().text = string.Concat(avatar.GetLingGeng[i]);
		}
		transform.Find("ziZhi").GetComponent<UILabel>().text = string.Concat(avatar.ZiZhi);
		transform.Find("wuXing").GetComponent<UILabel>().text = string.Concat(avatar.wuXin);
		transform.Find("ShenShi").GetComponent<UILabel>().text = string.Concat(avatar.shengShi);
		transform.Find("dunSu").GetComponent<UILabel>().text = string.Concat(avatar.dunSu);
		try
		{
			int xinJingLevel = avatar.GetXinJingLevel();
			int index = this.danduTypeIconID[avatar.GetDanDuLevel()];
			transform.Find("xinJin").GetComponent<UILabel>().text = string.Concat(new string[]
			{
				"[",
				ColorUtility.ToHtmlStringRGB(this.XinJInColor[xinJingLevel - 1]),
				"]",
				Tools.instance.Code64ToString(jsonData.instance.XinJinJsonData[xinJingLevel.ToString()]["Text"].str),
				"[-]"
			});
			transform.Find("dandu").GetComponent<UILabel>().text = string.Concat(new string[]
			{
				"[",
				ColorUtility.ToHtmlStringRGB(this.DanDuColor[index]),
				"]",
				Tools.instance.Code64ToString(jsonData.instance.DanduMiaoShu[(avatar.GetDanDuLevel() + 1).ToString()]["name"].str),
				"[-]"
			});
			transform.Find("dandu").GetComponent<TianFuCell>().text = Tools.instance.Code64ToString(jsonData.instance.DanduMiaoShu[(avatar.GetDanDuLevel() + 1).ToString()]["desc"].str);
			base.transform.Find("XinJin/Icon").GetComponent<UI2DSprite>().sprite2D = this.UIXinJin[xinJingLevel - 1];
			base.transform.Find("DanDu/Icon").GetComponent<UI2DSprite>().sprite2D = this.UIDanDu[index];
			base.transform.Find("JinDanShow/Sprite").GetComponent<UI2DSprite>().sprite2D = this.UIDengJi[(int)(avatar.level - 1)];
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		if (jsonData.instance.XinJinJsonData[avatar.GetXinJingLevel().ToString()]["Max"].n > 0f)
		{
			int xinJingLevel2 = avatar.GetXinJingLevel();
			if (xinJingLevel2 == 7)
			{
				base.transform.Find("XinJin/xinjinSlider/hpText").GetComponent<UILabel>().text = string.Format("{0}/-", avatar.xinjin);
			}
			else
			{
				base.transform.Find("XinJin/xinjinSlider/hpText").GetComponent<UILabel>().text = string.Format("{0}/{1}", avatar.xinjin, XinJinJsonData.DataDict[xinJingLevel2].Max);
			}
		}
		else
		{
			base.transform.Find("XinJin/xinjinSlider/hpText").GetComponent<UILabel>().text = string.Format("{0}/0", avatar.xinjin);
		}
		base.transform.Find("DanDu/xinjinSlider/hpText").GetComponent<UILabel>().text = ((avatar.Dandu + "/" + 120) ?? "");
		this.showStaticSkill();
		this.ShowSkill();
		this.ShowEquip();
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x000B37DC File Offset: 0x000B19DC
	public void showStaticSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Transform transform = base.transform.Find("short cut3").Find("Key");
		SkillStaticDatebase component = jsonData.instance.gameObject.GetComponent<SkillStaticDatebase>();
		int num = 0;
		foreach (object obj in transform)
		{
			Transform transform2 = (Transform)obj;
			bool flag = true;
			foreach (SkillItem skillItem in avatar.equipStaticSkillList)
			{
				if (skillItem.itemIndex == num)
				{
					flag = false;
					transform2.GetComponent<KeyCellMapPassSkill>().keySkill = component.dicSkills[Tools.instance.getStaticSkillKeyByID(skillItem.itemId)];
				}
			}
			if (flag)
			{
				transform2.GetComponent<KeyCellMapPassSkill>().keySkill = new GUIPackage.Skill();
			}
			num++;
		}
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x000B38FC File Offset: 0x000B1AFC
	public void ShowSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Transform transform = base.transform.Find("short cut4").Find("Key");
		SkillDatebase component = jsonData.instance.gameObject.GetComponent<SkillDatebase>();
		int num = 0;
		foreach (object obj in transform)
		{
			Transform transform2 = (Transform)obj;
			bool flag = true;
			foreach (SkillItem skillItem in avatar.equipSkillList)
			{
				if (skillItem.itemIndex == num)
				{
					flag = false;
					transform2.GetComponent<KeyCellMapSkill>().keySkill = component.dicSkills[Tools.instance.getSkillKeyByID(skillItem.itemId, Tools.instance.getPlayer())];
				}
			}
			if (flag)
			{
				transform2.GetComponent<KeyCellMapSkill>().keySkill = new GUIPackage.Skill();
			}
			num++;
		}
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000122C3 File Offset: 0x000104C3
	public void ShowEquip()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x000122D5 File Offset: 0x000104D5
	public void Close()
	{
		base.transform.localPosition = new Vector3(10000f, 10000f, 0f);
	}

	// Token: 0x04000F0B RID: 3851
	public List<Sprite> UIDanDu;

	// Token: 0x04000F0C RID: 3852
	public List<Sprite> UIXinJin;

	// Token: 0x04000F0D RID: 3853
	public List<Sprite> UIDengJi;

	// Token: 0x04000F0E RID: 3854
	public List<int> danduTypeIconID;

	// Token: 0x04000F0F RID: 3855
	public List<Color> DanDuColor;

	// Token: 0x04000F10 RID: 3856
	public List<Color> XinJInColor;
}
