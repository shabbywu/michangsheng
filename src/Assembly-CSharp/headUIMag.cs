using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

public class headUIMag : MonoBehaviour
{
	public List<Sprite> UIDanDu;

	public List<Sprite> UIXinJin;

	public List<Sprite> UIDengJi;

	public List<int> danduTypeIconID;

	public List<Color> DanDuColor;

	public List<Color> XinJInColor;

	private void Start()
	{
	}

	public void showHeadUI()
	{
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Transform val = ((Component)this).transform.Find("Text");
		string text = "0/年";
		foreach (SkillItem equipStaticSkill in avatar.equipStaticSkillList)
		{
			if (equipStaticSkill.itemIndex == 0)
			{
				Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
				text = ((int)avatar.getTimeExpSpeed()).ToString();
				break;
			}
		}
		((Component)val.Find("name")).GetComponent<UILabel>().text = avatar.name ?? "";
		((Component)val.Find("age")).GetComponent<UILabel>().text = string.Concat(avatar.age);
		((Component)val.Find("shouYuan")).GetComponent<UILabel>().text = string.Concat(avatar.shouYuan);
		((Component)val.Find("XiuLian")).GetComponent<UILabel>().text = text + "/月";
		((Component)val.Find("zhiwei")).GetComponent<UILabel>().text = PlayerEx.GetMenPaiChengHao();
		((Component)val.Find("fenglu")).GetComponent<UILabel>().text = avatar.chenghaomag.GetOneYearAddMoney() + "灵石/年";
		((Component)val.Find("shengwang")).GetComponent<UILabel>().text = PlayerEx.GetMenPaiShengWang().ToString();
		string text2 = Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[avatar.level.ToString()]["Name"].str) ?? "";
		((Component)((Component)this).transform.Find("LV/Label")).GetComponent<UILabel>().text = text2.Insert(2, "   ");
		UI2DSprite component = ((Component)((Component)this).transform.Find("LV/2D Sprite")).GetComponent<UI2DSprite>();
		component.sprite2D = Tools.instance.getLevelSprite(avatar.level, component.sprite2D.rect);
		((Component)((Component)this).transform.Find("MenPai/Label")).GetComponent<UILabel>().text = Tools.getStr("menpai" + avatar.menPai);
		int num = 0;
		foreach (int item in avatar.GetLingGeng)
		{
			num += item;
		}
		int num2 = avatar.GetLingGeng.Max();
		for (int i = 0; i < 5; i++)
		{
			((Component)((Component)this).transform.Find("LingGen/icon" + i)).GetComponent<UI2DSprite>().alpha = (float)avatar.GetLingGeng[i] / (float)num2;
			((Component)((Component)this).transform.Find("LingGen/zi" + i)).GetComponent<UILabel>().text = string.Concat(avatar.GetLingGeng[i]);
		}
		((Component)val.Find("ziZhi")).GetComponent<UILabel>().text = string.Concat(avatar.ZiZhi);
		((Component)val.Find("wuXing")).GetComponent<UILabel>().text = string.Concat(avatar.wuXin);
		((Component)val.Find("ShenShi")).GetComponent<UILabel>().text = string.Concat(avatar.shengShi);
		((Component)val.Find("dunSu")).GetComponent<UILabel>().text = string.Concat(avatar.dunSu);
		try
		{
			int xinJingLevel = avatar.GetXinJingLevel();
			int index = danduTypeIconID[avatar.GetDanDuLevel()];
			((Component)val.Find("xinJin")).GetComponent<UILabel>().text = "[" + ColorUtility.ToHtmlStringRGB(XinJInColor[xinJingLevel - 1]) + "]" + Tools.instance.Code64ToString(jsonData.instance.XinJinJsonData[xinJingLevel.ToString()]["Text"].str) + "[-]";
			((Component)val.Find("dandu")).GetComponent<UILabel>().text = "[" + ColorUtility.ToHtmlStringRGB(DanDuColor[index]) + "]" + Tools.instance.Code64ToString(jsonData.instance.DanduMiaoShu[(avatar.GetDanDuLevel() + 1).ToString()]["name"].str) + "[-]";
			((Component)val.Find("dandu")).GetComponent<TianFuCell>().text = Tools.instance.Code64ToString(jsonData.instance.DanduMiaoShu[(avatar.GetDanDuLevel() + 1).ToString()]["desc"].str);
			((Component)((Component)this).transform.Find("XinJin/Icon")).GetComponent<UI2DSprite>().sprite2D = UIXinJin[xinJingLevel - 1];
			((Component)((Component)this).transform.Find("DanDu/Icon")).GetComponent<UI2DSprite>().sprite2D = UIDanDu[index];
			((Component)((Component)this).transform.Find("JinDanShow/Sprite")).GetComponent<UI2DSprite>().sprite2D = UIDengJi[avatar.level - 1];
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		if (jsonData.instance.XinJinJsonData[avatar.GetXinJingLevel().ToString()]["Max"].n > 0f)
		{
			int xinJingLevel2 = avatar.GetXinJingLevel();
			if (xinJingLevel2 == 7)
			{
				((Component)((Component)this).transform.Find("XinJin/xinjinSlider/hpText")).GetComponent<UILabel>().text = $"{avatar.xinjin}/-";
			}
			else
			{
				((Component)((Component)this).transform.Find("XinJin/xinjinSlider/hpText")).GetComponent<UILabel>().text = $"{avatar.xinjin}/{XinJinJsonData.DataDict[xinJingLevel2].Max}";
			}
		}
		else
		{
			((Component)((Component)this).transform.Find("XinJin/xinjinSlider/hpText")).GetComponent<UILabel>().text = $"{avatar.xinjin}/0";
		}
		((Component)((Component)this).transform.Find("DanDu/xinjinSlider/hpText")).GetComponent<UILabel>().text = (avatar.Dandu + "/" + 120) ?? "";
		showStaticSkill();
		ShowSkill();
		ShowEquip();
	}

	public void showStaticSkill()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Transform obj = ((Component)this).transform.Find("short cut3").Find("Key");
		SkillStaticDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillStaticDatebase>();
		int num = 0;
		foreach (Transform item in obj)
		{
			Transform val = item;
			bool flag = true;
			foreach (SkillItem equipStaticSkill in avatar.equipStaticSkillList)
			{
				if (equipStaticSkill.itemIndex == num)
				{
					flag = false;
					((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill = component.dicSkills[Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId)];
				}
			}
			if (flag)
			{
				((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill = new GUIPackage.Skill();
			}
			num++;
		}
	}

	public void ShowSkill()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Transform obj = ((Component)this).transform.Find("short cut4").Find("Key");
		SkillDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillDatebase>();
		int num = 0;
		foreach (Transform item in obj)
		{
			Transform val = item;
			bool flag = true;
			foreach (SkillItem equipSkill in avatar.equipSkillList)
			{
				if (equipSkill.itemIndex == num)
				{
					flag = false;
					((Component)val).GetComponent<KeyCellMapSkill>().keySkill = component.dicSkills[Tools.instance.getSkillKeyByID(equipSkill.itemId, Tools.instance.getPlayer())];
				}
			}
			if (flag)
			{
				((Component)val).GetComponent<KeyCellMapSkill>().keySkill = new GUIPackage.Skill();
			}
			num++;
		}
	}

	public void ShowEquip()
	{
		_ = (Avatar)KBEngineApp.app.player();
	}

	public void Close()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localPosition = new Vector3(10000f, 10000f, 0f);
	}
}
