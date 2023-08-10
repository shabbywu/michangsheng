using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tab;

[Serializable]
public class TabWuDaoLevel : UIBase
{
	private List<TabWuDaoLevelBase> levelList;

	public int CurExp;

	private Text _wudaoExpText;

	private Image _slider;

	public TabWuDaoLevel(GameObject go, int id)
	{
		_go = go;
		_wudaoExpText = Get<Text>("BaseLevel/WuDaoValue");
		_slider = Get<Image>("Slider/JinDu");
		UpdateUI(id);
	}

	public void UpdateUI(int id)
	{
		CurExp = Tools.instance.getPlayer().wuDaoMag.getWuDaoEx(id).I;
		_slider.fillAmount = Tools.instance.getPlayer().wuDaoMag.getWuDaoExPercent(id) / 100f;
		_wudaoExpText.text = CurExp.ToString();
		if (levelList != null)
		{
			levelList.Clear();
		}
		levelList = new List<TabWuDaoLevelBase>();
		for (int i = 1; i <= 5; i++)
		{
			levelList.Add(new TabWuDaoLevelBase(Get($"Level{i}"), id, i));
		}
	}
}
