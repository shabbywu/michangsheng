using System;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Tab;

public class LevelTips : ITabTips
{
	private Text _levelTipsTitle;

	private Text _levelTipsMessage;

	public LevelTips(GameObject go)
	{
		_go = go;
		_rect = Get<RectTransform>("Bg");
		_sizeFitter = Get<ContentSizeFitter>("Bg");
		_childSizeFitter = Get<ContentSizeFitter>("Bg/Content");
		_levelTipsTitle = Get<Text>("Bg/Title/Text");
		_levelTipsMessage = Get<Text>("Bg/Content");
	}

	public void Show()
	{
		Avatar player = PlayerEx.Player;
		int jinDanID = getJinDanID();
		if (jinDanID == -1)
		{
			return;
		}
		JieDanBiao jieDanBiao = JieDanBiao.DataDict[jinDanID];
		_levelTipsTitle.text = jieDanBiao.JinDanQuality.ToCNNumber() + "品" + jieDanBiao.name;
		string text = $"<color=#c7c479>气血</color><color=#dbffa2>+ {player.getJieDanSkillAddHP()}</color>  ";
		text = text + "<color=#c7c479>修炼速度</color><color=#dbffa2>+" + $"{(int)(Math.Ceiling(player.getJieDanSkillAddExp() * 100f) - 100.0)}%</color>";
		for (int i = 0; i < jieDanBiao.LinGengType.Count; i++)
		{
			text = text + "\n<color=#c7c479>" + Tools.getStr("xibieFight" + jieDanBiao.LinGengType[i]) + "灵根权重</color>" + $"<color=#dbffa2>+{jieDanBiao.LinGengZongShu[i]}</color>";
		}
		if (jieDanBiao.desc != "")
		{
			text = text + "\n<color=#c7c479>" + jieDanBiao.desc + "</color>";
		}
		if (IsYuanYing())
		{
			_levelTipsTitle.text = "<color=#ce49ff>元婴</color>";
			foreach (SkillItem equipStaticSkill in player.equipStaticSkillList)
			{
				int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
				if (equipStaticSkill.itemIndex == 6)
				{
					text += "\n<color=#dbffa2>第二元神：元婴能够单独修炼一门功法，并根据功法属性解锁独有特性</color>";
					text = text + "\n<color=#dbffa2>元婴九变：" + player.getYuanYingStaticDesc(equipStaticSkill, staticSkillKeyByID) + "</color>";
					break;
				}
			}
		}
		if (IsHuaShen())
		{
			_levelTipsTitle.text = "<size=24><color=#ff7864>化神</color></size>";
			GUIPackage.Skill skill = SkillDatebase.instence.Dict[player.HuaShenLingYuSkill.I][1];
			text = text + "\n<color=#ff7864>" + skill.skill_Name + "</color>\n<color=#dbffa2>" + skill.skill_Desc + "</color>";
		}
		_levelTipsMessage.text = text;
		_go.SetActive(true);
		UpdateSize();
	}

	protected override string Replace(string msg)
	{
		return msg;
	}

	private int getJinDanID()
	{
		int result = -1;
		foreach (SkillItem hasJieDanSkill in Tools.instance.getPlayer().hasJieDanSkillList)
		{
			result = hasJieDanSkill.itemId;
		}
		return result;
	}

	private bool IsYuanYing()
	{
		return Tools.instance.getPlayer().level >= 10;
	}

	private bool IsHuaShen()
	{
		return Tools.instance.getPlayer().level >= 13;
	}
}
